using InvoiceImporter.Domain.Adapters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceImporter.Domain.Infra.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext? _context;

        public UnitOfWork(DataContext dataContext)
        {
            _context = dataContext;
        }

        public void BeginTransaction()
        {

        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
       
        }

        public void Rollback()
        {

        }
    }
}
