using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steam.Domain.Entities.Achievements;

namespace Steam.Infrastructure.Configurations.Achievements
{
    public class UserAchievementConfiguration : IEntityTypeConfiguration<UserAchievement>
    {
        public void Configure(EntityTypeBuilder<UserAchievement> builder)
        {
            builder.ToTable("UserAchievements");
            builder.HasKey(ua => ua.Id);

            // A user can earn a specific achievement only once.
            builder.HasIndex(ua => new { ua.UserId, ua.AchievementId }).IsUnique();

            builder.HasOne(ua => ua.User)
                   .WithMany()
                   .HasForeignKey(ua => ua.UserId);

            builder.HasOne(ua => ua.Achievement)
                   .WithMany()
                   .HasForeignKey(ua => ua.AchievementId);

            builder.HasQueryFilter(ua => !ua.IsDeleted);
        }
    }
}
