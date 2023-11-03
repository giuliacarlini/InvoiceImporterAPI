using InvoiceImporter.Domain.Adapters.Repository;
using InvoiceImporter.Domain.Entities;
using InvoiceImporter.Domain.Enum;

namespace ImporterInvoice.Tests.Mocks
{
    public class FakeInvoiceItemRepository : IInvoiceItemRepository
    {
        public void Add(IEnumerable<InvoiceItem> invoiceItems)
        {
            
        }

        public IEnumerable<InvoiceItem> FindAll(Guid Id)
        {
            return new List<InvoiceItem>();
        }
    }
}
