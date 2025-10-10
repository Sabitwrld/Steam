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

            builder.HasIndex(w => new { w.UserId, w.ApplicationId }).IsUnique();

            builder.HasOne(w => w.Application)
                   .WithMany()
                   .HasForeignKey(w => w.ApplicationId);

            // AppUser ilə əlaqə və silmə davranışının təyin edilməsi
            builder.HasOne(w => w.User)
                   .WithMany()
                   .HasForeignKey(w => w.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(w => !w.IsDeleted);
        }
    }
}
