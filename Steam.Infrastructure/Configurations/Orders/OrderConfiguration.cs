using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Orders;

namespace Steam.Infrastructure.Configurations.Orders
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.TotalPrice).HasColumnType("decimal(18, 2)").IsRequired();
            builder.Property(o => o.Status).IsRequired().HasMaxLength(50);

            builder.HasMany(o => o.Items)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId);

            // AppUser ilə əlaqə və silmə davranışının təyin edilməsi
            builder.HasOne(o => o.User)
                   .WithMany()
                   .HasForeignKey(o => o.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(o => !o.IsDeleted);
        }
    }
}
