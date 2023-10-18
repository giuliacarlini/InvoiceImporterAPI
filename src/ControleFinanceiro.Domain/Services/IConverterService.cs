using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;

namespace ControleFinanceiro.Domain.Services
{
    public interface IConverterService
    {
        Fatura TransformarLinhasEmObjeto(string caminhoArquivo,DateTime vencimento, TipoImportacao tipoImportacao);
    }
}
