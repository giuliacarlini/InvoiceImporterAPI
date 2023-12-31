﻿using Flunt.Notifications;
using ImporterInvoice.Tests.Common;
using InvoiceImporter.Domain.Entities;
using InvoiceImporter.Domain.Enum;

namespace ImporterInvoice.Tests.Tests.Entities
{
    public class InvoiceTests : Notifiable
    {
        public static readonly object[][] CorrectData =
        {
          new object[] {
              EImportType.Nubank,
              new DateTime(2023,10,18),
              "arquivo.csv"},
        };

        [Theory, MemberData(nameof(CorrectData))]
        public void testar_create_fatura_ler_csv(EImportType tipoImportacao, DateTime vencimento, string nomeArquivo)
        {

            var invoice = new Invoice(tipoImportacao, vencimento, nomeArquivo);

            string msg = string.Empty;
            foreach (var notification in Notifications)
            {
                msg += notification.ToString() + "; ";
            }

            if (Valid == false)
                Assert.Fail("O arquivo não foi validado: " + msg);

            invoice.ReadFileCSV(Const.Lines);

            Assert.NotNull(invoice);
            Assert.True(Guid.Empty != invoice.Id);
            Assert.True(vencimento == invoice.DueDate);
            Assert.True(invoice.RegisterDate.Date == DateTime.Now.Date);
            Assert.True(invoice.InvoiceItems?.Count > 0);
            Assert.True(invoice.ImportType == EImportType.Nubank);
            Assert.True(invoice.FileName == invoice.FileName);
        }

        [Theory, MemberData(nameof(CorrectData))]
        public void testar_create_fatura_create_lancamento(EImportType tipoImportacao, DateTime vencimento, string nomeArquivo)
        {
            var fatura = new Invoice(tipoImportacao, vencimento, nomeArquivo);

            var lancamento = new InvoiceItem(tipoImportacao, "2023-10-20,", fatura.Id);

            Assert.True(lancamento.Invalid);
            fatura.AddInvoiceItem(lancamento);

            Assert.True(fatura.InvoiceItems?.Last().Id != Guid.Empty);
            Assert.True(fatura.InvoiceItems?.Last().Category == "");
            Assert.True(fatura.InvoiceItems?.Last().Date == new DateTime(2023, 10, 20));
        }
    }
}
