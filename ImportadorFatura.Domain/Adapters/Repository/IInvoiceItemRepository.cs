using ImporterInvoice.Domain.Entities;

namespace ImporterInvoice.Domain.Adapters
{
    public interface IInvoiceItemRepository
    {
        void Save(InvoiceItem invoiceItem);
    }
}
