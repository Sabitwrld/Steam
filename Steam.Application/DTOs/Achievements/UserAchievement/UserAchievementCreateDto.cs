namespace Steam.Application.DTOs.Achievements.UserAchievement
{
    /// <summary>
    /// Represents the data needed to mark an achievement as unlocked for a user.
    /// This would typically be called by the game client or a backend service.
    /// </summary>
    public record UserAchievementCreateDto
    {
        /// <summary>
        /// The ID of the user who unlocked the achievement.
        /// </summary>
        public string UserId { get; init; } = default!;

        /// <summary>
        /// The ID of the achievement that was unlocked.
        /// </summary>
        public int AchievementId { get; init; }
    }
}
