using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;

namespace ControleFinanceiro.UnitTests.Entities
{
    public class FaturaTests
    {
        public static readonly object[][] CorrectData =
        {
          new object[] { 
              TipoImportacao.Nubank, 
              new DateTime(2023,10,18),  
              "TestedeNomedeArquivocommaisde50caracteresTesteTesteTeste.csv"},

          new object[] {
              TipoImportacao.C6Bank,
              new DateTime(2023,10,18),
              ""}
        };

        [Theory, MemberData(nameof(CorrectData))]
        public void testar_create_fatura(TipoImportacao tipoImportacao, DateTime dateTime, string nomeArquivo)
        {

            var faturaTest = new Fatura(tipoImportacao, dateTime, nomeArquivo);

            

            Assert.NotNull(faturaTest);
            Assert.True(Guid.Empty != faturaTest.IdFatura);
            Assert.True(dateTime == faturaTest.Vencimento);
            Assert.True(faturaTest.DataHoraCadastro.Date == DateTime.Now.Date);
            Assert.True(nomeArquivo == faturaTest.NomeArquivo);
            Assert.NotNull(faturaTest.Lancamentos);
        }

    }
}
