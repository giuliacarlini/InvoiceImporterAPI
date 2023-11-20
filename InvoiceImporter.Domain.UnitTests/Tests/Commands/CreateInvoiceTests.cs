using ImporterInvoice.Domain.Shared.Commands;
using ImporterInvoice.Tests.Common;
using ImporterInvoice.Tests.Mocks;
using InvoiceImporter.Domain.Commands;
using InvoiceImporter.Domain.Commands.Request;
using InvoiceImporter.Domain.Commands.Response;
using InvoiceImporter.Domain.Enum;
using InvoiceImporter.Domain.Handlers;
using InvoiceImporter.Domain.Infra.Context;
using InvoiceImporter.Domain.Settings;
using InvoiceImporter.Domain.Tests.Tests.Mocks;
using Microsoft.Extensions.Options;

namespace ImporterInvoice.Tests.Tests.Commands
{
    public class CreateInvoiceTests
    {
        IOptions<AppSettings> appSettings;

        public CreateInvoiceTests() 
        {
            appSettings = Options.Create(new AppSettings());
        }

        [Fact]
        public void testar_criar_fatura_command_fast_validation_sucesso()
        {
            var FaturaCommand = new CreateInvoiceRequest()
            {
                ImportType = EImportType.Nubank,
                DueDate = new DateTime(2023, 10, 26),
                FileName = "C:\\Teste\\teste.csv"
            };

            FaturaCommand.Validate();

            Assert.True(FaturaCommand.Valid);
        }

        [Fact]
        public void testar_criar_fatura_command_fast_validation_falha()
        {
            var FaturaCommand = new CreateInvoiceRequest()
            {
                ImportType = EImportType.Nubank,
                DueDate = new DateTime(2023, 10, 26),
                FileName = "",
                Lines = Const.Lines
            };

            FaturaCommand.Validate();

            Assert.True(FaturaCommand.Invalid);
        }

        [Fact]
        public void testar_criar_fatura_processar_sem_items()
        {
            var FaturaCommand = new CreateInvoiceRequest()
            {
                ImportType = EImportType.Nubank,
                DueDate = new DateTime(2023, 10, 26),
                FileName = "C:\\Teste\\teste.csv"
            };

            var unitOfWork = new FakeUnitOfWork();

            var invoiceHandler = new InvoiceHandler(
                new FakeInvoiceRepository(),
                new FakeInvoiceItemRepository(),
                appSettings,
                unitOfWork);

            var response = (CommandResponse)invoiceHandler.Handle(FaturaCommand);

            Assert.False(response.Success);
        }

        [Fact]
        public void testar_criar_fatura_processar_com_items()
        {
            var FaturaCommand = new CreateInvoiceRequest()
            {
                ImportType = EImportType.Nubank,
                DueDate = new DateTime(2023, 10, 26),
                FileName = "C:\\Teste\\teste.csv"
            };

            var unitOfWork =  new FakeUnitOfWork();

            var invoiceHandler = new InvoiceHandler(
                new FakeInvoiceRepository(),
                new FakeInvoiceItemRepository(),
                appSettings,
                unitOfWork);

            var response = (CommandResponse)invoiceHandler.Handle(FaturaCommand);

            Assert.False(response.Success);
        }
    }
}
