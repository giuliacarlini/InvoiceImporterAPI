using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Adapters
{
    public interface IImportacaoRepository
    {
        Importacao Save(Importacao fatura);
    }
}
