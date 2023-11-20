using InvoiceImporter.Domain.Adapters.Repository;
using InvoiceImporter.Domain.Entities;
using InvoiceImporter.Domain.Enum;
using InvoiceImporter.Domain.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace InvoiceImporter.Domain.Infra.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DataContext _context;

        public InvoiceRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Invoice invoice)
        {
            _context.Add(invoice);
        }

        public IEnumerable<Invoice> FindAll()
        {        
            return _context.Invoice.ToList();
        }

        public bool FindInvoice(string nameFile)
        {
            return _context.Invoice.Where(x => x.FileName == nameFile).ToList().Count > 0;
        }

        public bool FindInvoice(DateTime dueDate, EImportType importType)
        {
            return _context.Invoice.Where(x => x.DueDate.Date == dueDate.Date && x.ImportType == importType).ToList().Count > 0;
        }

        
    }
}
