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
    public class RefundConfiguration : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.ToTable("Refunds");
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Amount).HasColumnType("decimal(18, 2)");
            builder.Property(r => r.Reason).IsRequired().HasMaxLength(1000);
            builder.Property(r => r.Status).IsRequired().HasMaxLength(50);

            // A Payment can have multiple Refunds
            builder.HasOne(r => r.Payment)
                   .WithMany() // Assuming Payment doesn't need a direct collection of Refunds
                   .HasForeignKey(r => r.PaymentId);

            builder.HasQueryFilter(r => !r.IsDeleted);
        }
    }
}
