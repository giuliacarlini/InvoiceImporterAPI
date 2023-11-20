using InvoiceImporter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceImporter.Domain.Infra.Mapping
{
    public class InvoiceMap : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoice");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ImportType)
                .HasColumnName("ImportType")
                .HasColumnType("int")
                .IsRequired(true);

            builder.Property(x => x.DueDate)
                .HasColumnName("DueDate")
                .HasColumnType("DateTime")
                .IsRequired(true);

            builder.Property(x => x.RegisterDate)
                .HasColumnName("RegisterDate")
                .HasColumnType("DateTime")
                .IsRequired(true);
       
            builder.Ignore(x => x.Notifications);
        }
    }
}
