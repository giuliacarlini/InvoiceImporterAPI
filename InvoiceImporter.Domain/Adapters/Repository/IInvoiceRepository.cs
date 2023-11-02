using InvoiceImporter.Domain.Entities;
using InvoiceImporter.Domain.Enum;

namespace InvoiceImporter.Domain.Adapters.Repository
{
    public interface IInvoiceRepository
    {
        void Add(Invoice invoice);
        bool FindInvoice(string nameFile);
        bool FindInvoice(DateTime dueDate, EImportType importType);
    }
}