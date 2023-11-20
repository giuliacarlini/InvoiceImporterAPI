using ImporterInvoice.Tests.Common;
using ImporterInvoice.Tests.Mocks;
using InvoiceImporter.Domain.Commands;
using InvoiceImporter.Domain.Commands.Request;
using InvoiceImporter.Domain.Enum;
using InvoiceImporter.Domain.Handlers;
using InvoiceImporter.Domain.Infra.Context;
using InvoiceImporter.Domain.Settings;
using InvoiceImporter.Domain.Tests.Tests.Mocks;
using Microsoft.Extensions.Options;

namespace ImporterInvoice.Tests.Handlers
{
    public class CreateInvoiceHandlersTests
    {
        IOptions<AppSettings> appSettings;

        public CreateInvoiceHandlersTests()
        {
            appSettings = Options.Create(new AppSettings());
        }

        [Fact]
        public void testar_importacao_fatura_nubank_invalido()
        {
            var unitOfWork = new FakeUnitOfWork();

            var handlerFatura = new InvoiceHandler(
                new FakeInvoiceRepository(),
                new FakeInvoiceItemRepository(),
                appSettings,
                unitOfWork);

            var command = new CreateInvoiceRequest()
            {
                ImportType = EImportType.Nubank,
                FileName = "C:\\Teste\\Teste.csv",
                DueDate = new DateTime(2023, 10, 26)
            };

            handlerFatura.Handle(command);

            Assert.True(handlerFatura.Invalid);
        }

        [Fact]
        public void testar_importacao_fatura_nubank_valido()
        {
            var unitOfWork = new FakeUnitOfWork();

            var handlerFatura = new InvoiceHandler(
                new FakeInvoiceRepository(),
                new FakeInvoiceItemRepository(),
                appSettings,
                unitOfWork);

            var command = new CreateInvoiceRequest()
            {
                ImportType = EImportType.Nubank,
                FileName = "arquivo.csv",
                DueDate = new DateTime(2023, 10, 26),
                Lines = Const.Lines
            };

            handlerFatura.Handle(command);

            Assert.True(handlerFatura.Valid);
        }
    }
}
