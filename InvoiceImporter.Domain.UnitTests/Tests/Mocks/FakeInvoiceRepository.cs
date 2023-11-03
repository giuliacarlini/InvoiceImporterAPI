using InvoiceImporter.Domain.Adapters.Repository;
using InvoiceImporter.Domain.Entities;
using InvoiceImporter.Domain.Enum;

namespace ImporterInvoice.Tests.Mocks
{
    public class FakeInvoiceRepository : IInvoiceRepository
    {
        public void Add(Invoice fatura)
        {

        }

        public IEnumerable<Invoice> FindAll()
        {
            throw new NotImplementedException();
        }

        public bool FindInvoice(string nomeArquivo)
        {
            return false;
        }

        public bool FindInvoice(DateTime vencimento, EImportType tipoImportacao)
        {
            return false;
        }
    }
}
