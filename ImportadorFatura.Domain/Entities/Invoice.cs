using ImporterInvoice.Domain.Enum;
using ImporterInvoice.Domain.ValueObjects;
using ImporterInvoice.Shared.Entities;

namespace ImporterInvoice.Domain.Entities
{
    public class Invoice : Entity
    {
        private List<InvoiceItem> _invoiceItems;

        public Invoice(EImportType importType, DateTime dueDate, FilePath filePath)
        {
            InvoiceType = importType;
            DueDate = dueDate;
            RegisterDate = DateTime.Now;
            FilePath = filePath;
            _invoiceItems = new List<InvoiceItem>();

            AddNotifications(filePath);      
        }

        public EImportType InvoiceType { get; private set; }

        public DateTime DueDate { get; private set; }

        public DateTime RegisterDate { get; private set; }

        public FilePath FilePath { get; private set; }

        public IReadOnlyCollection<InvoiceItem>? InvoiceItems { get { return _invoiceItems.ToArray(); } }

        public void AddInvoiceItem(InvoiceItem invoiceItem)
        {
            _invoiceItems.Add(invoiceItem);
        }

        public void ReadFileCSV()
        {
            List<string> file = File.ReadLines(FilePath.ToString()).ToList();

            foreach (var lines in file.Skip(1))
            {
                var invoiceItem = new InvoiceItem(InvoiceType, lines);
                AddInvoiceItem(invoiceItem);
            }

            if (_invoiceItems.Count == 0)
                AddNotification("Itens da Fatura", "Não foram encontrados itens na fatura");
        }
    }
}
