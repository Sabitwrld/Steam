using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Configurations.Orders
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Amount).HasColumnType("decimal(18, 2)");
            builder.Property(p => p.Method).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Status).IsRequired().HasMaxLength(50);
            builder.Property(p => p.TransactionId).HasMaxLength(255);
            builder.HasIndex(p => p.TransactionId);

            // One-to-One relationship between Order and Payment
            builder.HasOne(p => p.Order)
                   .WithOne(o => o.Payment)
                   .HasForeignKey<Payment>(p => p.OrderId);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
