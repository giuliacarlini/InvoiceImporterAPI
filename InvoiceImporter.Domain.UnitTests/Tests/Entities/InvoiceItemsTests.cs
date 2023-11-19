using Flunt.Notifications;
using InvoiceImporter.Domain.Entities;
using InvoiceImporter.Domain.Enum;

namespace ImporterInvoice.Tests.Tests.Entities
{
    public class InvoiceItemsTests
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
            var invoice = new Invoice();

            var invoiceItem = new InvoiceItem(EImportType.Nubank, linha, invoice.Id);

            invoice.AddInvoiceItem(invoiceItem);

            Assert.NotNull(invoiceItem);
            Assert.True(invoiceItem.Date == data, "Data inválida");
            Assert.True(invoiceItem.Category == categoria, "Categoria inválida");
            Assert.True(invoiceItem.Description == descricao, "Descrição inválida");
            Assert.True(invoiceItem.CurrentyInstallments == parcela, "Parcela inválida");
            Assert.True(invoiceItem.TotalInstallments == totalParcela, "Total Parcela inválida");
            Assert.True(invoiceItem.Value == valor, "Valor inválido");
            Assert.True(invoiceItem.InvoiceId == invoice.Id, "Id da Fatura está inválido");
        }

        [Fact]
        public void testar_create_lancamento_invalido()
        {
            var invoice = new Invoice();

            var invoiceitem = new InvoiceItem(EImportType.Nubank, "2019-10-20,,", invoice.Id) ;

            Assert.True(invoiceitem.Invalid);
            Assert.True(ValidarEntidades("Data", InvoiceItem.InvalidDate, invoiceitem.Notifications));
            Assert.True(ValidarEntidades("Categoria", InvoiceItem.InvalidCategory, invoiceitem.Notifications));
            Assert.True(ValidarEntidades("Descricao", InvoiceItem.InvalidDescription, invoiceitem.Notifications));
            Assert.True(ValidarEntidades("Valor", InvoiceItem.InvalidValue, invoiceitem.Notifications));
        }

        private bool ValidarEntidades(string property, string errorMessage, IReadOnlyCollection<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                if (notification.Property == property)
                {
                    return notification.Message.Contains(errorMessage);
                }
            }

            return false;
        }
    }

}
