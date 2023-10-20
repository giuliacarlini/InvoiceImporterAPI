using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;
using ControleFinanceiro.Domain.ValueObjects;
using Flunt.Notifications;

namespace ControleFinanceiro.UnitTests.Entities
{
    public class FaturaTests : Notifiable
    {
        public static readonly object[][] CorrectData =
        {
          new object[] { 
              ETipoImportacao.Nubank, 
              new DateTime(2023,10,18),
              Environment.CurrentDirectory + "\\Arquivos\\nubank\\nubank-2023-09.csv"},
        };

        [Theory, MemberData(nameof(CorrectData))]
        public void testar_create_fatura_ler_csv(ETipoImportacao tipoImportacao, DateTime vencimento, string nomeArquivo)
        {
            var caminhoArquivo = new CaminhoArquivo(nomeArquivo);
            var fatura = new Fatura(tipoImportacao, vencimento, caminhoArquivo);

            if (Valid == false)
                return;

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
            fatura.AdicionarLancamento(lancamento);

            Assert.True(fatura.Lancamentos?.Last().Id != Guid.Empty);
            Assert.True(fatura.Lancamentos?.Last().Categoria == "");
            Assert.True(fatura.Lancamentos?.Last().Data == new DateTime(2023, 10, 20));
        }

    }
}
