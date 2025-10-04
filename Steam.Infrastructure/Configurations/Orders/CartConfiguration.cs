using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Orders;

namespace Steam.Infrastructure.Configurations.Orders
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(c => c.Id);

            // A Cart has many CartItems
            builder.HasMany(c => c.Items)
                   .WithOne(ci => ci.Cart)
                   .HasForeignKey(ci => ci.CartId);

            // Note: Relationship for UserId to AppUser is handled by Identity's conventions
            // but we can add an index for performance
            builder.HasIndex(c => c.UserId).IsUnique();

            builder.HasQueryFilter(c => !c.IsDeleted);
        }
    }
}
