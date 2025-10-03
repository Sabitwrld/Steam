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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Price).HasColumnType("decimal(18, 2)").IsRequired();
            builder.Property(oi => oi.Quantity).IsRequired();

            // Relationship to ApplicationCatalog
            builder.HasOne(oi => oi.Application)
                   .WithMany()
                   .HasForeignKey(oi => oi.ApplicationId)
                   .OnDelete(DeleteBehavior.Restrict); // Do not delete an application if it is in an order history

            builder.HasQueryFilter(oi => !oi.IsDeleted);

        }
    }
}
