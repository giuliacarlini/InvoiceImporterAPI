using ImporterInvoice.Domain.Commands;
using ImporterInvoice.Domain.Enum;

namespace ImporterInvoice.Tests.Tests.Commands
{
    public class CreateInvoiceCommandTests
    {
        [Fact]
        public void testar_criar_fatura_command_sucesso()
        {
            var FaturaCommand = new CreateInvoiceCommand()
            {
                ImportType = EImportType.Nubank,
                DueDate = new DateTime(2023, 10, 26),
                FilePath = "C:\\Teste\\teste.csv"
            };

            FaturaCommand.Validate();

            Assert.True(FaturaCommand.Valid);
        }

        [Fact]
        public void testar_criar_fatura_command_falha()
        {
            var FaturaCommand = new CreateInvoiceCommand()
            {
                ImportType = EImportType.Nubank,
                DueDate = new DateTime(2023, 10, 26),
                FilePath = "C:\\Teste\\Teste\\teste.csv"
            };

            FaturaCommand.Validate();

            Assert.True(FaturaCommand.Invalid);
        }
    }
}
