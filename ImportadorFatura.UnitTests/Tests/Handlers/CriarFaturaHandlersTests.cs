using ImportadorFatura.Domain.Commands;
using ImportadorFatura.Domain.Handlers;
using ImportadorFatura.Tests.Common;
using ImportadorFatura.Tests.Mocks;

namespace ImportadorFatura.Tests.Handlers
{
    public class CriarFaturaHandlersTests
    {
        [Fact]
        public void testar_importacao_fatura_nubank_invalido()
        {
            var handlerFatura = new CriarFaturaHandler(new FakeFaturaRepository());

            var command = new CriarFaturaCommand()
            {
                TipoImportacao = Domain.Enum.ETipoImportacao.Nubank,
                CaminhoArquivo = "C:\\Teste\\Teste.csv",
                Vencimento = new DateTime(2023, 10, 26)
            };

            handlerFatura.Handle(command);

            Assert.True(handlerFatura.Invalid);
        }

        [Fact]
        public void testar_importacao_fatura_nubank_valido()
        {
            var handlerFatura = new CriarFaturaHandler(new FakeFaturaRepository());


            var command = new CriarFaturaCommand()
            {
                TipoImportacao = Domain.Enum.ETipoImportacao.Nubank,
                CaminhoArquivo = new Variaveis().RetornarCaminhoArquivoValido(),
                Vencimento = new DateTime(2023, 10, 26)
            };

            handlerFatura.Handle(command);

            Assert.True(handlerFatura.Valid);
        }
    }
}
