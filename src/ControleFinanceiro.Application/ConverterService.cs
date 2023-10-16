using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;
using ControleFinanceiro.Domain.Services;

namespace ControleFinanceiro.Application
{
    public class ConverterService : IConverterService
    {
        public async Task<Fatura> TransformarLinhasEmObjeto(string caminhoArquivo, DateTime vencimento, TipoImportacao tipoImportacao)
        {
            var nomeArquivo = caminhoArquivo.Split('\\').Last();            

            if (Directory.Exists(caminhoArquivo.Replace(nomeArquivo, "")) == false)
                throw new DirectoryNotFoundException("Diretório de arquivo não encontrado: " + caminhoArquivo);

            if (File.Exists(caminhoArquivo) == false)
                throw new FileNotFoundException("Arquivo não encontrado: " + caminhoArquivo);

            if (Path.GetExtension(caminhoArquivo) != ".csv")
                throw new FileLoadException("A extensão de arquivo é inválida para a importação: " + caminhoArquivo);

            try
            {
                var data = File.ReadLines(caminhoArquivo);

                if (!data.Any())
                    throw new Exception("Registros não encontrados");

                List<Lancamento>? listaLancamentos = new List<Lancamento>();

                var fatura = new Fatura() { DataHoraCadastro = DateTime.Now, IdOrigem = (int)tipoImportacao, Vencimento = vencimento, NomeArquivo = Path.GetFileName(caminhoArquivo) };

                foreach (var line in data.Skip(1))
                {
                    var lancamentoImportacao = new LancamentoImportacao(line);

                    switch (tipoImportacao)
                    {
                        case TipoImportacao.Nubank:                            
                            listaLancamentos.Add(GerarLancamentoNubank(lancamentoImportacao, line));
                            break;
                    }
                }

                fatura.Lancamentos = listaLancamentos;

                if (fatura.Lancamentos.Count == 0)
                    throw new Exception("Não encontrado registros válidos para o tipo de importação escolhido.");

                return fatura;
            }
            catch (FormatException ex)
            {
                throw new FormatException("Erro ao converter linhas em objetos: " + ex.Message, ex.InnerException);
            }
        }

        private Lancamento GerarLancamentoNubank(LancamentoImportacao lancamentoImportacao, string line)
        {
            var lineSplitNu = line.Split(",");

            return new Lancamento()
            {
                Data = DateTime.Parse(lineSplitNu[0]),
                Categoria = lineSplitNu[1],
                Descricao = lineSplitNu[2],
                Valor = decimal.Parse(lineSplitNu[3].Replace(".", ","), System.Globalization.NumberStyles.Currency),
                Parcelado = LocalizarParcela(lineSplitNu[2], false) != "",
                Parcela = LocalizarParcela(lineSplitNu[2], false),
                TotalParcela = LocalizarParcela(lineSplitNu[2], true),
                LancamentoImportacao = lancamentoImportacao
            };
        }

        private string LocalizarParcela(string descricao, bool TotalParcela)
        {
            try
            {
                if (descricao.IndexOf("/") > 0)
                {
                    string[] retornoSplit = descricao.Split(' ');

                    foreach (string s in retornoSplit)
                    {
                        if (s.Contains("/"))
                        {
                            var resultado = TotalParcela ? s.Substring(s.IndexOf("/") + 1, new string(s.Reverse().ToArray()).IndexOf("/")) : s.Substring(0, s.IndexOf("/"));

                            return int.Parse(resultado).ToString();
                        }
                    }
                }
            }
            catch
            {
                return "";
            }

            return "";
        }
    }
}
