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
        public string FilePath { get; set; } = string.Empty;
        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsTrue(Path.Exists(FilePath), "CaminhoArquivo", "O Caminho está inválido!")
                .IsTrue(File.Exists(FilePath), "CaminhoArquivo", "O Arquivo não existe!")
                .IsNotNull(ImportType, "TipoExportacao", "O Tipo da Exportação está inválido!")
                .IsBetween(DueDate, new DateTime(2020, 1, 1), DateTime.Now, "Vencimento", "O Vencimento está inválido!"));
        }
    }
}
