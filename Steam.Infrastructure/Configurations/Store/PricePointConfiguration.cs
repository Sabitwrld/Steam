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
    public class PricePointConfiguration : IEntityTypeConfiguration<PricePoint>
    {
        public void Configure(EntityTypeBuilder<PricePoint> builder)
        {
            builder.ToTable("PricePoints");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.BasePrice).HasColumnType("decimal(18, 2)");

            builder.HasOne(p => p.Application)
                   .WithMany() // Assuming Application doesn't need a direct collection of PricePoints
                   .HasForeignKey(p => p.ApplicationId);

            // One PricePoint has many RegionalPrices
            builder.HasMany(p => p.RegionalPrices)
                   .WithOne(r => r.PricePoint)
                   .HasForeignKey(r => r.PricePointId);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
