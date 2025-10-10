using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Steam.Domain.Entities.ReviewsRating;

namespace Steam.Infrastructure.Configurations.ReviewsRating
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");
            builder.HasKey(r => r.Id);

            builder.HasIndex(r => new { r.UserId, r.ApplicationId }).IsUnique();
            builder.Property(r => r.Title).HasMaxLength(150);
            builder.Property(r => r.Content).IsRequired().HasMaxLength(8000);
            builder.Property(r => r.IsRecommended).IsRequired();
            builder.Property(r => r.HelpfulCount).HasDefaultValue(0);
            builder.Property(r => r.FunnyCount).HasDefaultValue(0);

            // AppUser ilə əlaqə və silmə davranışının təyin edilməsi
            builder.HasOne(r => r.User)
                   .WithMany()
                   .HasForeignKey(r => r.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Application)
                   .WithMany()
                   .HasForeignKey(r => r.ApplicationId)
                   .IsRequired();

            builder.HasQueryFilter(r => !r.IsDeleted);
        }
    }
}
