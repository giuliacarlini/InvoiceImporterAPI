using ImporterInvoice.Domain.Entities;
using InvoiceImporter.Domain.Infra.Mapping;
using Microsoft.EntityFrameworkCore;

namespace InvoiceImporter.Domain.Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceItem> InvoiceItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new InvoiceMap().Configure(modelBuilder.Entity<Invoice>());
            new InvoiceItemMap().Configure(modelBuilder.Entity<InvoiceItem>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
