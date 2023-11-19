using InvoiceImporter.Domain.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceImporter.Domain.Tests.Tests.Mocks
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public void BeginTransaction()
        {

        }

        public void Commit()
        {

        }

        public void Dispose()
        {
        }

        public void Rollback()
        {

        }
    }
}
