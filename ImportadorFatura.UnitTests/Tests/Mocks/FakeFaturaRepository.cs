using ImportadorFatura.Domain.Adapters.Repository;
using ImportadorFatura.Domain.Entities;
using ImportadorFatura.Domain.Enum;

namespace ImportadorFatura.Tests.Mocks
{
    public class FakeFaturaRepository : IFaturaRepository
    {
        public void Adicionar(Fatura fatura)
        {

        }

        public bool BuscarFatura(string nomeArquivo)
        {
            return false;
        }

        public bool BuscarFatura(DateTime vencimento, ETipoImportacao tipoImportacao)
        {
            return false;
        }
    }
}
