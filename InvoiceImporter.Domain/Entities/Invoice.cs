using ImporterInvoice.Domain.Shared.Entities;
using InvoiceImporter.Domain.Enum;
using InvoiceImporter.Domain.ValueObjects;

namespace InvoiceImporter.Domain.Entities
{
    public class Invoice : Entity
    {
        private List<InvoiceItem>? _invoiceItems;

        public Invoice() { }

        public Invoice(EImportType importType, DateTime dueDate, FilePath filePath)
        {
            ImportType = importType;
            DueDate = dueDate;
            RegisterDate = DateTime.Now;
            FilePath = filePath;
            _invoiceItems = new List<InvoiceItem>();

            AddNotifications(filePath);
        }

        public EImportType ImportType { get; private set; }

        public DateTime DueDate { get; private set; }

        public DateTime RegisterDate { get; private set; }

        public FilePath? FilePath { get; private set; }

        public IReadOnlyCollection<InvoiceItem>? InvoiceItems { get { return _invoiceItems?.ToArray(); } }

        public void AddInvoiceItem(InvoiceItem invoiceItem)
        {
            _invoiceItems?.Add(invoiceItem);
        }

        public void ReadFileCSV()
        {
            if (FilePath is not null)
            {
                List<string> file = File.ReadLines(FilePath.ToString()).ToList();

                foreach (var lines in file.Skip(1))
                {
                    var invoiceItem = new InvoiceItem(ImportType, lines);
                    AddInvoiceItem(invoiceItem);
                }
            }

            if (_invoiceItems?.Count == 0)
                AddNotification("Itens da Fatura", "Não foram encontrados itens na fatura");
        }

    }
}
