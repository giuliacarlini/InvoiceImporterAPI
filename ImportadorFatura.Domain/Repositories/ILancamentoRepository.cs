using ImportadorFatura.Domain.Entities;

namespace ImportadorFatura.Domain.Repositories
{
    public interface ILancamentoRepository
    {
        void CriarLancamento(Lancamento lancamento);
    }
}
