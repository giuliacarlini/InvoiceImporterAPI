using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Adapters.Repository
{
    public interface IFaturaRepository
    {
        Fatura Adicionar(Fatura fatura);
        bool BuscarFaturaPorNomeArquivo(string nomeArquivo);
    }
}