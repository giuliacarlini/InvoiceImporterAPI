using ControleFinanceiroAPI.Model;

namespace ControleFinanceiroAPI.Repositories.Interface
{
    public interface IImportacaoRepository
    {
        int Save(Importacao fatura);
    }
}
