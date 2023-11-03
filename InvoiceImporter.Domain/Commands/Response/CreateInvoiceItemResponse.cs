namespace InvoiceImporter.Domain.Commands.Response
{
    public class CreateInvoiceItemResponse
    {
        public CreateInvoiceItemResponse() { }
        public DateTime Date { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string CurrentyInstallments { get; set; } = string.Empty;
        public string TotalInstallments { get; set; } = string.Empty;
    }
}


