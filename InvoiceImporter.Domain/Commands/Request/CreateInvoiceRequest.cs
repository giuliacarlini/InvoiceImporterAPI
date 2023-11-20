using Flunt.Notifications;
using Flunt.Validations;
using ImporterInvoice.Domain.Shared.Commands;
using InvoiceImporter.Domain.Enum;

namespace InvoiceImporter.Domain.Commands.Request
{
    public class CreateInvoiceRequest : Notifiable, ICommand
    {
        public EImportType ImportType { get; set; }
        public DateTime DueDate { get; set; }
        public string FileName { get; set; } = string.Empty;
        public List<string> Lines { get; set; } = new List<string>();
        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FileName, 8, "FileName", "O Nome do Arquivo está inválido!")
                .IsNotNull(ImportType, "TipoImportacao", "O Tipo da Importação está inválido!")
                .IsBetween(DueDate, new DateTime(2020, 1, 1), DateTime.Now, "Vencimento", "O Vencimento está inválido!"));
        }
    }
}
