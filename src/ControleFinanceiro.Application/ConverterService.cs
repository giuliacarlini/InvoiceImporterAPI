using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;
using ControleFinanceiro.Domain.Services;

namespace ControleFinanceiro.Application
{
    public class ConverterService : IConverterService
    {
        public Fatura TransformarLinhasEmObjeto(string caminhoArquivo, DateTime vencimento, TipoImportacao tipoImportacao)
        {
            var arquivo = ValidarRegistros(caminhoArquivo);

            try
            {
                var _fatura = new Fatura(tipoImportacao, vencimento, Path.GetFileName(caminhoArquivo));

                foreach (var linha in arquivo.Skip(1))
                {
                    var lancamentoImportacao = new LancamentoImportacao(linha);
                    _fatura.Lancamentos.Add(new Lancamento(lancamentoImportacao, linha, tipoImportacao));
                }

                if (_fatura.Lancamentos.Count == 0)
                    throw new Exception("Não encontrado registros válidos para o tipo de importação escolhido.");

                return _fatura;
            }
            catch (FormatException ex)
            {
                throw new FormatException("Erro ao converter linhas em objetos: " + ex.Message, ex.InnerException);
            }
        }

        private IEnumerable<string> ValidarRegistros(string caminhoArquivo)
        {
            var nomeArquivo = caminhoArquivo.Split('\\').Last();

            if (Directory.Exists(caminhoArquivo.Replace(nomeArquivo, "")) == false)
                throw new DirectoryNotFoundException("Diretório de arquivo não encontrado: " + caminhoArquivo);

            if (File.Exists(caminhoArquivo) == false)
                throw new FileNotFoundException("Arquivo não encontrado: " + caminhoArquivo);

            if (Path.GetExtension(caminhoArquivo) != ".csv")
                throw new FileLoadException("A extensão de arquivo é inválida para a importação: " + caminhoArquivo);

            var data = File.ReadLines(caminhoArquivo);

            return data == null ? throw new Exception("Registros não encontrados") : data;
        }
    }
}
