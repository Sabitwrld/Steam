using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Steam.Domain.Entities.ReviewsRating; // 1. Add this using statement to specify which 'Review' you mean.

namespace Steam.Infrastructure.Configurations.ReviewsRating // 2. Namespace corrected.
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");
            builder.HasKey(r => r.Id);

            // A user can only write one review per application
            builder.HasIndex(r => new { r.UserId, r.ApplicationId }).IsUnique();

            builder.Property(r => r.Title)
                   .HasMaxLength(150);

            builder.Property(r => r.Content)
                   .IsRequired()
                   .HasMaxLength(8000); // Steam's review character limit

            builder.Property(r => r.IsRecommended)
                   .IsRequired();

            builder.Property(r => r.HelpfulCount)
                   .HasDefaultValue(0);

            builder.Property(r => r.FunnyCount)
                   .HasDefaultValue(0);

            // Relationship with AppUser
            builder.HasOne(r => r.User)
                   .WithMany() // Assuming AppUser doesn't need a direct collection of Reviews
                   .HasForeignKey(r => r.UserId)
                   .IsRequired();

            // Relationship with ApplicationCatalog
            builder.HasOne(r => r.Application)
                   .WithMany() // Assuming ApplicationCatalog doesn't need a direct collection of Reviews
                   .HasForeignKey(r => r.ApplicationId)
                   .IsRequired();

            builder.HasQueryFilter(r => !r.IsDeleted);
        }
    }
}
