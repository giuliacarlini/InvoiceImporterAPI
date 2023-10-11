using ControleFinanceiro.Domain.Entities;

namespace ControleFinanceiro.Domain.Adapters
{
    public interface IFaturaRepository
    {
        Fatura Adicionar(Fatura fatura);
        bool BuscarFaturaPorNomeArquivo(string nomeArquivo);
    }
}