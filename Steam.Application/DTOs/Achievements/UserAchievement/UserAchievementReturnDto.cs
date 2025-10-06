using Steam.Application.DTOs.Achievements.Achievements;

namespace Steam.Application.DTOs.Achievements.UserAchievement
{
    /// <summary>
    /// Represents the full details of an unlocked achievement for a user.
    /// </summary>
    public record UserAchievementReturnDto
    {
        public int Id { get; init; }
        public string UserId { get; init; } = default!;
        public DateTime DateUnlocked { get; init; }

        /// <summary>
        /// Includes the full details of the unlocked achievement.
        /// </summary>
        public AchievementReturnDto Achievement { get; init; } = default!;
    }
}
