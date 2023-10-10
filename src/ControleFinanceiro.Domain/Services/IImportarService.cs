using ControleFinanceiro.Domain.Enum;

namespace ControleFinanceiro.Domain.Services
{
    public interface IImportarService
    {
        int ImportarArquivo(string CaminhoArquivo, DateTime Vencimento, TipoImportacao TipoImportacao);
    }
}
