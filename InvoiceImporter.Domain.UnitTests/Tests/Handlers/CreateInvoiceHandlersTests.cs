using ImporterInvoice.Tests.Common;
using ImporterInvoice.Tests.Mocks;
using InvoiceImporter.Domain.Commands;
using InvoiceImporter.Domain.Commands.Request;
using InvoiceImporter.Domain.Enum;
using InvoiceImporter.Domain.Handlers;

namespace ImporterInvoice.Tests.Handlers
{
    public class CreateInvoiceHandlersTests
    {
        [Fact]
        public void testar_importacao_fatura_nubank_invalido()
        {
            var handlerFatura = new InvoiceHandler(
                new FakeInvoiceRepository(),
                new FakeInvoiceItemRepository()
            );

            var command = new CreateInvoiceRequest()
            {
                ImportType = EImportType.Nubank,
                FilePath = "C:\\Teste\\Teste.csv",
                DueDate = new DateTime(2023, 10, 26)
            };

            handlerFatura.Handle(command);

            Assert.True(handlerFatura.Invalid);
        }

        [Fact]
        public void testar_importacao_fatura_nubank_valido()
        {
            var handlerFatura = new InvoiceHandler(
                new FakeInvoiceRepository(),
                new FakeInvoiceItemRepository()
                );

            var command = new CreateInvoiceRequest()
            {
                ImportType = EImportType.Nubank,
                FilePath = Const.CaminhoArquivoValido,
                DueDate = new DateTime(2023, 10, 26)
            };

            handlerFatura.Handle(command);

            Assert.True(handlerFatura.Valid);
        }
    }
}
