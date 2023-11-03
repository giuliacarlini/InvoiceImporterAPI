using InvoiceImporter.Domain.Entities;

namespace InvoiceImporter.Domain.Adapters.Repository
{
    public interface IInvoiceItemRepository
    {
        void Add(IEnumerable<InvoiceItem> invoiceItems);

        IEnumerable<InvoiceItem> FindAll(Guid Id);
    }
}
