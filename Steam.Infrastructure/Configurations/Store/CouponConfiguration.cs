using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Configurations.Store
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("Coupons");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(c => c.Code).IsUnique(); // Ensure coupon codes are unique

            builder.Property(c => c.DiscountPercent)
                .HasColumnType("decimal(5, 2)");

            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
