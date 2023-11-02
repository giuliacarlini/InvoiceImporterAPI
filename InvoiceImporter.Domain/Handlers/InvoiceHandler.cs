using Flunt.Notifications;
using ImporterInvoice.Domain.Shared.Commands;
using ImporterInvoice.Domain.Shared.Handlers;
using InvoiceImporter.Domain.Adapters.Repository;
using InvoiceImporter.Domain.Commands;
using InvoiceImporter.Domain.Entities;
using InvoiceImporter.Domain.ValueObjects;

namespace InvoiceImporter.Domain.Handlers
{
    public class InvoiceHandler : Notifiable, IHandler<CreateInvoiceCommand>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository;

        public InvoiceHandler(IInvoiceRepository invoiceRepository,
            IInvoiceItemRepository invoiceItemRepository)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
        }

        public ICommandResult Handle(CreateInvoiceCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new CommandResult(
                    false,
                    "Não foi possível importar a fatura!",
                    command.Notifications);

            if (_invoiceRepository.FindInvoice(command.FilePath))
                AddNotification("CaminhoArquivo", "O Arquivo já foi adicionado anteriormente.");

            if (_invoiceRepository.FindInvoice(command.DueDate, command.ImportType))
                AddNotification("Arquivo", "Já foi importado um arquivo para mesma data de Vencimento e tipo");

            var filePath = new FilePath(command.FilePath);

            var invoice = new Invoice(command.ImportType, command.DueDate, filePath);

            invoice.ReadFileCSV();

            AddNotifications(invoice, filePath);

            if (command.Invalid)
                return new CommandResult(
                    false,
                    "Não foi possível importar a fatura.",
                    command.Notifications);

            if (invoice.InvoiceItems is not null)
                _invoiceItemRepository.Add(invoice.InvoiceItems);

            _invoiceRepository.Add(invoice);

            return new CommandResult(
                true,
                "Fatura importada com sucesso!",
                invoice);
        }
    }
}
