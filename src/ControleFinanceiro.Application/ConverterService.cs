using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;
using ControleFinanceiro.Domain.Services;
using ControleFinanceiro.Domain.ValueObjects;

namespace ControleFinanceiro.Application
{
    public class ConverterService : IConverterService
    {
        public Fatura TransformarLinhasEmObjeto(string caminhoArquivo, DateTime vencimento, TipoImportacao tipoImportacao)
        {
            try
            {
                var _fatura = new Fatura(tipoImportacao, vencimento, new CaminhoArquivo(caminhoArquivo));

                _fatura.LerArquivoCSV();

                if (_fatura.Lancamentos?.Count == 0)
                    throw new Exception("Não encontrado registros válidos para o tipo de importação escolhido.");

                return _fatura;
            }
            catch (FormatException ex)
            {
                throw new FormatException("Erro ao converter linhas em objetos: " + ex.Message, ex.InnerException);
            }
        }
    }
}
