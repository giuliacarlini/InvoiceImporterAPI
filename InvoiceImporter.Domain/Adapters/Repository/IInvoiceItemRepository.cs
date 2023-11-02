using InvoiceImporter.Domain.Entities;

namespace InvoiceImporter.Domain.Adapters.Repository
{
    public interface IInvoiceItemRepository
    {
        void Save(InvoiceItem invoiceItem);
    }
}
