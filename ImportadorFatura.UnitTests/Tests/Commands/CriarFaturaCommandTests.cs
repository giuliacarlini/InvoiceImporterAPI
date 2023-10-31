using ImportadorFatura.Domain.Commands;
using ImportadorFatura.Domain.Enum;

namespace ImportadorFatura.Tests.Tests.Commands
{
    public class CriarFaturaCommandTests
    {
        [Fact]
        public void testar_criar_fatura_command_sucesso()
        {
            var FaturaCommand = new CreateFaturaCommand()
            {
                TipoImportacao = ETipoImportacao.Nubank,
                Vencimento = new DateTime(2023, 10, 26),
                CaminhoArquivo = "C:\\Teste\\teste.csv"
            };

            FaturaCommand.Validar();

            Assert.True(FaturaCommand.Valid);
        }

        [Fact]
        public void testar_criar_fatura_command_falha()
        {
            var FaturaCommand = new CreateFaturaCommand()
            {
                TipoImportacao = ETipoImportacao.Nubank,
                Vencimento = new DateTime(2023, 10, 26),
                CaminhoArquivo = "C:\\Teste\\Teste\\teste.csv"
            };

            FaturaCommand.Validar();

            Assert.True(FaturaCommand.Invalid);
        }
    }
}
