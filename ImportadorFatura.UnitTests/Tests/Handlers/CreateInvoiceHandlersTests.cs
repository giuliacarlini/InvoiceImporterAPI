using ImporterInvoice.Domain.Commands;
using ImporterInvoice.Domain.Handlers;
using ImporterInvoice.Tests.Common;
using ImporterInvoice.Tests.Mocks;

namespace ImporterInvoice.Tests.Handlers
{
    public class CreateInvoiceHandlersTests
    {
        [Fact]
        public void testar_importacao_fatura_nubank_invalido()
        {
            var handlerFatura = new CreateInvoiceHandler(new FakeInvoiceRepository());

            var command = new CreateInvoiceCommand()
            {
                ImportType = Domain.Enum.EImportType.Nubank,
                FilePath = "C:\\Teste\\Teste.csv",
                DueDate = new DateTime(2023, 10, 26)
            };

            handlerFatura.Handle(command);

            Assert.True(handlerFatura.Invalid);
        }

        [Fact]
        public void testar_importacao_fatura_nubank_valido()
        {
            var handlerFatura = new CreateInvoiceHandler(new FakeInvoiceRepository());


            var command = new CreateInvoiceCommand()
            {
                ImportType = Domain.Enum.EImportType.Nubank,
                FilePath = new Variaveis().RetornarCaminhoArquivoValido(),
                DueDate = new DateTime(2023, 10, 26)
            };

            handlerFatura.Handle(command);

            Assert.True(handlerFatura.Valid);
        }
    }
}
