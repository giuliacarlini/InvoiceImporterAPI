using Flunt.Notifications;
using Flunt.Validations;
using ImporterInvoice.Domain.Enum;
using ImporterInvoice.Domain.ValueObjects;
using ImporterInvoice.Shared.Commands;

namespace ImporterInvoice.Domain.Commands
{
    public class CreateInvoiceCommand : Notifiable, ICommand
    {
        public EImportType ImportType { get; set; }
        public DateTime DueDate { get; set; }
        public string FilePath { get; set; } = string.Empty;

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Path.Exists(FilePath), "CaminhoArquivo", "O Caminho está inválido!")
                .IsNotNull(ImportType, "TipoExportacao", "O Tipo da Exportação está inválido!")
                .IsBetween(DueDate, new DateTime(2020,1,1), DateTime.Now, "Vencimento", "O Vencimento está inválido!"));
        }
    }
}
