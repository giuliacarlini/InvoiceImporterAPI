using ControleFinanceiroAPI.Model;

namespace ControleFinanceiroAPI.Repositories.Interface
{
    public interface ILancamentoRepository
    {
        void Save(Lancamento lacamento);
    }
}
