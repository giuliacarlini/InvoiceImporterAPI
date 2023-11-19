using InvoiceImporter.Domain.Entities;
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
                .IsRequired(true)
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .IsRequired(true)
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Value)
                .HasColumnName("Value")
                .IsRequired(true)
                .HasPrecision(14,2);

            builder.Property(x => x.CurrentyInstallments)
                .HasColumnName("CurrentyInstallments")
                .IsRequired(false)
                .HasColumnType("char(2)");

            builder.Property(x => x.TotalInstallments)
                .HasColumnName("TotalInstallments")
                .IsRequired(false)
                .HasColumnType("char(2)");

            builder.Ignore(x => x.Notifications);
        }
    }
}
