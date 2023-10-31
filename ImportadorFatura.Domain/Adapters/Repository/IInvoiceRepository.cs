using ImporterInvoice.Domain.Entities;
using ImporterInvoice.Domain.Enum;

namespace ImporterInvoice.Domain.Adapters.Repository
{
    public interface IInvoiceRepository
    {
        void Add(Invoice invoice);
        bool FindInvoice(string nameFile);
        bool FindInvoice(DateTime dueDate, EImportType importType);
    }
}