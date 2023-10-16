using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Adapters
{
    public interface ILancamentoImportacaoRepository
    {
        LancamentoImportacao Adicionar(LancamentoImportacao fatura);
    }
}
