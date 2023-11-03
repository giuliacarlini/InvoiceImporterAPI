﻿using Flunt.Notifications;
using ImporterInvoice.Domain.Shared.Commands;
using ImporterInvoice.Domain.Shared.Handlers;
using InvoiceImporter.Domain.Adapters.Repository;
using InvoiceImporter.Domain.Commands.Request;
using InvoiceImporter.Domain.Commands.Response;
using InvoiceImporter.Domain.Entities;
using InvoiceImporter.Domain.ValueObjects;

namespace InvoiceImporter.Domain.Handlers
{
    public class InvoiceHandler : Notifiable, IHandler<CreateInvoiceRequest>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository;

        public InvoiceHandler(IInvoiceRepository invoiceRepository,
            IInvoiceItemRepository invoiceItemRepository)
        {
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
        }

        public ICommandResult Handle(FindAllInvoicesRequest request) 
        {
            var listResponse =  new List<CreateInvoiceResponse>();

            var listInvoices = _invoiceRepository.FindAll();

            foreach (var invoice in listInvoices)
            {
                var invoiceItems = _invoiceItemRepository.FindAll(invoice.Id);
                var listCreateInvoiceItemResponse = new List<CreateInvoiceItemResponse>();

                foreach (var item in invoiceItems)
                {
                    var invoiceItemResponse = new CreateInvoiceItemResponse()
                    {
                        Date = DateTime.Now,
                        Category = item.Category,
                        Description = item.Description,
                        CurrentyInstallments = item.CurrentyInstallments,
                        TotalInstallments = item.TotalInstallments
                    };

                    listCreateInvoiceItemResponse.Add(invoiceItemResponse);
                }


                var _invoiceResponse = new CreateInvoiceResponse()
                {
                    ImportType = invoice.ImportType,
                    DueDate = invoice.DueDate,
                    RegisterDate = invoice.RegisterDate,
                    Items = listCreateInvoiceItemResponse
                };
                listResponse.Add(_invoiceResponse);
            }

            return new CommandResponse(
                true,
                "Consulta realizada com sucesso!",
                listResponse);
        }


        public ICommandResult Handle(CreateInvoiceRequest command)
        {
            command.Validate();
            
            if (_invoiceRepository.FindInvoice(Path.GetFileName(command.FilePath)))
                AddNotification("CaminhoArquivo", "O Arquivo já foi adicionado anteriormente.");

            if (_invoiceRepository.FindInvoice(command.DueDate, command.ImportType))
                AddNotification("Arquivo", "Já foi importado um arquivo para mesma data de Vencimento e tipo");

            var _filePath = new FilePath(command.FilePath);

            var _invoice = new Invoice(command.ImportType, command.DueDate, _filePath);

            AddNotifications(command, _filePath);

            if (Invalid)
                return RetornarInvalido();

            _invoice.ReadFileCSV();

            AddNotifications(_invoice);

            if (Valid)
            {
                var itemsResponse = new List<CreateInvoiceItemResponse>();

                if (_invoice.InvoiceItems is not null)
                {
                    _invoiceItemRepository.Add(_invoice.InvoiceItems);

                    foreach (var item in _invoice.InvoiceItems)
                    {
                        var itemResponse = new CreateInvoiceItemResponse()
                        {
                            Date = item.Date,
                            Category = item.Category,
                            Description = item.Description,
                            CurrentyInstallments = item.CurrentyInstallments,
                            TotalInstallments = item.TotalInstallments,
                            Value = item.Value
                        };

                        itemsResponse.Add(itemResponse);
                    }

                    _invoiceRepository.Add(_invoice);
                }

                return new CommandResponse()
                {
                    Success = true,
                    Message = "Fatura importada com sucesso.",
                    Data = new CreateInvoiceResponse()
                    {
                        DueDate = _invoice.DueDate,
                        RegisterDate = _invoice.RegisterDate,
                        ImportType = _invoice.ImportType,
                        Items = itemsResponse
                    }
                };
            }
            else 
            {
                return RetornarInvalido();
            }
        }

        private ICommandResult RetornarInvalido()
        {
            return new CommandResponse(
                false,
                "Não foi possível importar a fatura.",
                Notifications);
        }
    }
}
