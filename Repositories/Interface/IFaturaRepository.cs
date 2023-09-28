using ControleFinanceiroAPI.Model;

namespace ControleFinanceiroAPI.Repositories.Interface
{
    public interface IFaturaRepository
    {
        int Save(Fatura fatura);
    }
}