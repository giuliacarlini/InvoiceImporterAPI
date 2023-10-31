using ImportadorFatura.Domain.Entities;
using ImportadorFatura.Domain.Enum;
using ImportadorFatura.Domain.ValueObjects;
using Flunt.Notifications;
using ImportadorFatura.Tests.Common;

namespace ImportadorFatura.Tests.Tests.Entities
{
    public class FaturaTests : Notifiable
    {


        public static readonly object[][] CorrectData =
        {
          new object[] {
              ETipoImportacao.Nubank,
              new DateTime(2023,10,18),
              new Variaveis().RetornarCaminhoArquivoValido()},
        };

        [Theory, MemberData(nameof(CorrectData))]
        public void testar_create_fatura_ler_csv(ETipoImportacao tipoImportacao, DateTime vencimento, string nomeArquivo)
        {
            var caminhoArquivo = new CaminhoArquivo(nomeArquivo);
            var fatura = new Fatura(tipoImportacao, vencimento, caminhoArquivo);

            string msg = string.Empty;
            foreach (var notification in Notifications)
            {
                msg += notification.ToString() + "; ";
            }

            if (Valid == false)
                Assert.Fail("O arquivo não foi validado: " + msg);

            fatura.LerArquivoCSV();

            Assert.NotNull(fatura);
            Assert.True(Guid.Empty != fatura.Id);
            Assert.True(vencimento == fatura.Vencimento);
            Assert.True(fatura.DataHoraCadastro.Date == DateTime.Now.Date);
            Assert.True(fatura.Lancamentos?.Count > 0);
            Assert.True(fatura.TipoImportacao == ETipoImportacao.Nubank);
            Assert.True(fatura.CaminhoArquivo.Caminho == fatura.CaminhoArquivo.Diretorio + fatura.CaminhoArquivo.Nome);
        }

        [Theory, MemberData(nameof(CorrectData))]
        public void testar_create_fatura_create_lancamento(ETipoImportacao tipoImportacao, DateTime vencimento, string nomeArquivo)
        {
            var caminhoArquivo = new CaminhoArquivo(nomeArquivo);
            var fatura = new Fatura(tipoImportacao, vencimento, caminhoArquivo);

            var lancamento = new Lancamento(tipoImportacao, "2023-10-20,");

            Assert.True(lancamento.Invalid);
            fatura.AdicionarLancamento(lancamento);


            Assert.True(fatura.Lancamentos?.Last().Id != Guid.Empty);
            Assert.True(fatura.Lancamentos?.Last().Categoria == "");
            Assert.True(fatura.Lancamentos?.Last().Data == new DateTime(2023, 10, 20));
        }
    }
}
