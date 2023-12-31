﻿using InvoiceImporter.Domain.Adapters.Repository;
using InvoiceImporter.Domain.Entities;
using InvoiceImporter.Domain.Infra.Context;

namespace InvoiceImporter.Domain.Infra.Repositories
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly DataContext _context;

        public InvoiceItemRepository(DataContext context) 
        { 
            _context = context;
        
        }
        public void Add(IEnumerable<InvoiceItem> invoiceItems)
        {
            _context.AddRange(invoiceItems);          
        }

        public IEnumerable<InvoiceItem> FindAll(Guid Id)
        {
            return _context.InvoiceItem.Where(x => x.InvoiceId == Id).ToList();
        }
    }
}
