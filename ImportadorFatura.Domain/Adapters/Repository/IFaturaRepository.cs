using ImportadorFatura.Domain.Entities;
using ImportadorFatura.Domain.Enum;

namespace ImportadorFatura.Domain.Adapters.Repository
{
    public interface IFaturaRepository
    {
        void Adicionar(Fatura fatura);
        bool BuscarFatura(string nomeArquivo);
        bool BuscarFatura(DateTime vencimento, ETipoImportacao tipoImportacao);
    }
}