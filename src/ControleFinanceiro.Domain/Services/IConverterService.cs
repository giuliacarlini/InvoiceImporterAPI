using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;

namespace ControleFinanceiro.Domain.Services
{
    public interface IConverterService
    {
        Task<Fatura> TransformarLinhasEmObjeto(string caminhoArquivo,DateTime vencimento, TipoImportacao tipoImportacao);
    }
}
