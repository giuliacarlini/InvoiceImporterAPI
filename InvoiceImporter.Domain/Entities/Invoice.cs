using ImporterInvoice.Domain.Shared.Entities;
using InvoiceImporter.Domain.Enum;

namespace InvoiceImporter.Domain.Entities
{
    public class Invoice : Entity
    {
        private List<InvoiceItem>? _invoiceItems;

        public Invoice() { }

        public Invoice(EImportType importType, DateTime dueDate, string filePath)
        {
            ImportType = importType;
            DueDate = dueDate;
            RegisterDate = DateTime.Now;
            FileName = filePath;
            _invoiceItems = new List<InvoiceItem>();
        }

        public EImportType ImportType { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public string FileName { get; private set; }
        public IReadOnlyCollection<InvoiceItem>? InvoiceItems { get { return _invoiceItems?.ToArray(); } }

        public void AddInvoiceItem(InvoiceItem invoiceItem)
        {
            _invoiceItems?.Add(invoiceItem);
        }

        public void ReadFileCSV(List<string> lines)
        {
            if (FileName is not null)
            {
                foreach (var line in lines.Skip(1))
                {
                    var invoiceItem = new InvoiceItem(ImportType, line, Id);
                    
                    AddInvoiceItem(invoiceItem);
                }
            }

            if (_invoiceItems?.Count == 0)
                AddNotification("Itens da Fatura", "Não foram encontrados itens na fatura");
        }

    }
}
