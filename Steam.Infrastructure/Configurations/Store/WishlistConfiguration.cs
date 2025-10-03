using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Store;

namespace Steam.Infrastructure.Configurations.Store
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {
            builder.ToTable("Wishlists");
            builder.HasKey(w => w.Id);

            // To prevent adding the same game to the wishlist multiple times for the same user
            builder.HasIndex(w => new { w.UserId, w.ApplicationId }).IsUnique();

            builder.HasOne(w => w.Application)
                   .WithMany()
                   .HasForeignKey(w => w.ApplicationId);

            // Note: Relationship for UserId to AppUser is configured by Identity implicitly

            builder.HasQueryFilter(w => !w.IsDeleted);
        }
    }
}
