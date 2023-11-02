using InvoiceImporter.Domain.Adapters.Repository;
using InvoiceImporter.Domain.Entities;
using InvoiceImporter.Domain.Enum;

namespace ImporterInvoice.Tests.Mocks
{
    public class FakeInvoiceRepository : IInvoiceRepository
    {
        public void Add(Invoice fatura)
        {

        }

        public bool FindInvoice(string nomeArquivo)
        {
            return false;
        }

        public bool FindInvoice(DateTime vencimento, EImportType tipoImportacao)
        {
            return false;
        }
    }
}
