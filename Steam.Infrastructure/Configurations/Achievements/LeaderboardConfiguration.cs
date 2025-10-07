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

            // A user can have only one entry per application leaderboard.
            builder.HasIndex(l => new { l.ApplicationId, l.UserId }).IsUnique();

            builder.HasOne(l => l.Application)
                   .WithMany()
                   .HasForeignKey(l => l.ApplicationId);

            builder.HasOne(l => l.User)
                   .WithMany()
                   .HasForeignKey(l => l.UserId);

            builder.HasQueryFilter(l => !l.IsDeleted);
        }
    }
}
