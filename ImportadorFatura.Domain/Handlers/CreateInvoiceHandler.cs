using Flunt.Notifications;
using ImporterInvoice.Domain.Adapters.Repository;
using ImporterInvoice.Domain.Commands;
using ImporterInvoice.Domain.Entities;
using ImporterInvoice.Domain.ValueObjects;
using ImporterInvoice.Shared.Commands;
using ImporterInvoice.Shared.Handlers;

namespace ImporterInvoice.Domain.Handlers
{
    public class CreateInvoiceHandler : Notifiable, IHandler<CreateInvoiceCommand>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public CreateInvoiceHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public ICommandResult Handle(CreateInvoiceCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new CommandResult() { Success = false, Message = command.Notifications.Last().Message };

            if (_invoiceRepository.FindInvoice(command.FilePath))
                AddNotification("CaminhoArquivo", "O Arquivo já foi adicionado anteriormente.");

            if (_invoiceRepository.FindInvoice(command.DueDate, command.ImportType))
                AddNotification("Arquivo", "Já foi importado um arquivo para mesma data de Vencimento e tipo");

            var filePath = new FilePath(command.FilePath);

            var fatura = new Invoice(command.ImportType, command.DueDate, filePath);         

            fatura.ReadFileCSV();

            AddNotifications(fatura, filePath);

            if (Invalid)
                return new CommandResult() { Success = false, Message = "Não foi possível importar a fatura." };

            return new CommandResult() { Success = true, Message = "" };
        }
    }
}
