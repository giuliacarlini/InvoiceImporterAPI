using ControleFinanceiroAPI.Utils;

namespace ControleFinanceiroAPI.Interface
{
    public interface IImportar
    {
        bool ImportarArquivo(string CaminhoArquivo, DateTime Vencimento, TipoImportacao TipoImportacao);
    }
}
