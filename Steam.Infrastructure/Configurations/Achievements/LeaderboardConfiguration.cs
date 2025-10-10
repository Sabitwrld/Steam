using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Achievements;

namespace Steam.Infrastructure.Configurations.Achievements
{
    public class LeaderboardConfiguration : IEntityTypeConfiguration<Leaderboard>
    {
        public void Configure(EntityTypeBuilder<Leaderboard> builder)
        {
            builder.ToTable("Leaderboards");
            builder.HasKey(l => l.Id);

            builder.HasIndex(l => new { l.ApplicationId, l.UserId }).IsUnique();

            builder.HasOne(l => l.Application)
                   .WithMany()
                   .HasForeignKey(l => l.ApplicationId);

            // AppUser ilə əlaqə və silmə davranışının təyin edilməsi
            builder.HasOne(l => l.User)
                   .WithMany()
                   .HasForeignKey(l => l.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(l => !l.IsDeleted);
        }
    }
}
