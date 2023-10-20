using ControleFinanceiro.Domain.Entities;
using ControleFinanceiro.Domain.Enum;

namespace ControleFinanceiro.UnitTests.Entities
{
    public class LancamentoTests
    {
        public static readonly object[][] CorrectData =
        {
          new object[] {
              "2023-08-05,transporte,Pag * Accomerciodegas 2/2,156.5",
                new DateTime(2023, 08, 05),
                "transporte",
                "Pag * Accomerciodegas 2/2",
                "2",
                "2",
                new decimal(156.5)},

          new object[] {
              "2023-08-05,serviços,Picpay *Barellaservic 6/10,132.41",
                new DateTime(2023, 08, 05),
                "serviços",
                "Picpay *Barellaservic 6/10",
                "6",
                "10",
                new decimal(132.41)},

          new object[] {
              "2023-08-05,serviços teste,Picpay 100/10,2000.00",
                new DateTime(2023, 08, 05),
                "serviços teste",
                "Picpay 100/10",
                "100",
                "10",
                new decimal(2000.00)},

          new object[] {
              "2023-08-05,,Picpay 0/0,-100.00",
                new DateTime(2023, 08, 05),
                "",
                "Picpay 0/0",
                "0",
                "0",
                new decimal(-100.00)},

          new object[] {
              "2023-08-05,,         Picpay 0/0,-100.00",
                new DateTime(2023, 08, 05),
                "",
                "Picpay 0/0",
                "0",
                "0",
                new decimal(-100.00)},
        };

        [Theory, MemberData(nameof(CorrectData))]
        public void test_lancamentos_validos(string linha, DateTime data, string categoria, string descricao, string parcela, string totalParcela, decimal valor)
        {
            var lancamento = new Lancamento(ETipoImportacao.Nubank, linha);

            Assert.NotNull(lancamento);
            Assert.True(lancamento.Data == data, "Data inválida");
            Assert.True(lancamento.Categoria == categoria, "Categoria inválida");
            Assert.True(lancamento.Descricao == descricao, "Descrição inválida");
            Assert.True(lancamento.Parcela == parcela, "Parcela inválida");
            Assert.True(lancamento.TotalParcela == totalParcela, "Total Parcela inválida");
            Assert.True(lancamento.Valor == valor, "Valor inválido");
        }

    }          
    
}
