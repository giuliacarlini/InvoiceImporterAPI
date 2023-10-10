using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Adapters
{
    public interface IFaturaRepository
    {
        Fatura Save(Fatura fatura);
        bool BuscarFaturaPorNomeArquivo(string nomeArquivo);
    }
}