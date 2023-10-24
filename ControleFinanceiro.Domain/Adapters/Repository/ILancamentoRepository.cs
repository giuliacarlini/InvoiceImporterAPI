using ImportadorFatura.Domain.Entities;

namespace ImportadorFatura.Domain.Adapters
{
    public interface ILancamentoRepository
    {
        void Save(Lancamento lacamento);
    }
}
