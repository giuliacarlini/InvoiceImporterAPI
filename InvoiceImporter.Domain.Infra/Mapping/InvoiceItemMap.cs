using ImporterInvoice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceImporter.Domain.Infra.Mapping
{
    public class InvoiceItemMap : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.ToTable("InvoiceItem");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Date)
                .HasColumnName("Date")
                .IsRequired(true);

            builder.Property(x => x.Category)
                .HasColumnName("Category")
                .IsRequired(true);

            builder.Property(x => x.Value)
                .HasColumnName("Value")
                .IsRequired(true);

            builder.Property(x => x.CurrentyInstallments)
                .HasColumnName("CurrentyInstallments")
                .IsRequired(false);

            builder.Property(x => x.TotalInstallments)
                .HasColumnName("TotalInstallments")
                .IsRequired(false);

        }
    }
}
