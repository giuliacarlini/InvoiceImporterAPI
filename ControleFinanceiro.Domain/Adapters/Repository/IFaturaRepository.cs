using ImportadorFatura.Domain.Entities;

namespace ImportadorFatura.Domain.Adapters.Repository
{
    public interface IFaturaRepository
    {
        Fatura Adicionar(Fatura fatura);
        bool BuscarFaturaPorNomeArquivo(string nomeArquivo);
    }
}