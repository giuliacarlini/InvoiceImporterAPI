using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Adapters
{
    public interface ILancamentoRepository
    {
        void Save(Lancamento lacamento);
    }
}
