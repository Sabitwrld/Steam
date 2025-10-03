using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Store;

namespace Steam.Infrastructure.Configurations.Store
{
    public class RegionalPriceConfiguration : IEntityTypeConfiguration<RegionalPrice>
    {
        public void Configure(EntityTypeBuilder<RegionalPrice> builder)
        {
            builder.ToTable("RegionalPrices");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Currency).IsRequired().HasMaxLength(3); // e.g., AZN, USD
            builder.Property(r => r.Amount).HasColumnType("decimal(18, 2)");
            builder.HasQueryFilter(r => !r.IsDeleted);
        }
    }
}
