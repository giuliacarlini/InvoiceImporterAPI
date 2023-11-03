using InvoiceImporter.Domain.Enum;

namespace InvoiceImporter.Domain.Commands.Response
{
    public class CreateInvoiceResponse
    {
        public CreateInvoiceResponse() { }
        public EImportType ImportType { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public IEnumerable<CreateInvoiceItemResponse>? Items { get; set; }
    }
}


