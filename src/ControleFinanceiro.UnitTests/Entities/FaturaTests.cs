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
              TipoImportacao.Nubank, 
              new DateTime(2023,10,18),
              Environment.CurrentDirectory + "\\Arquivos\\nubank\\nubank-2023-09.csv"},

        };

        [Theory, MemberData(nameof(CorrectData))]
        public void testar_create_fatura(TipoImportacao tipoImportacao, DateTime vencimento, string nomeArquivo)
        {
            var caminhoArquivo = new CaminhoArquivo(nomeArquivo);
            var fatura = new Fatura(tipoImportacao, vencimento, caminhoArquivo);

            if (Valid == false)
                return;

            fatura.LerArquivoCSV();

            Assert.NotNull(fatura);
            Assert.True(Guid.Empty != fatura.IdFatura);
            Assert.True(vencimento == fatura.Vencimento);
            Assert.True(fatura.DataHoraCadastro.Date == DateTime.Now.Date);

            Assert.True(fatura.Lancamentos?.Count() > 0);
        }

    }
}
