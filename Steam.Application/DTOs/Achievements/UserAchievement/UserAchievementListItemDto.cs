namespace Steam.Application.DTOs.Achievements.UserAchievement
{
    /// <summary>
    /// Represents a summary of an achievement unlocked by a user, suitable for lists.
    /// </summary>
    public record UserAchievementListItemDto
    {
        public int Id { get; init; }

        /// <summary>
        /// The name of the unlocked achievement.
        /// </summary>
        public string AchievementName { get; init; } = default!;

        /// <summary>
        /// The icon of the unlocked achievement.
        /// </summary>
        public string AchievementIconUrl { get; init; } = string.Empty;

        /// <summary>
        /// The date when the user unlocked this achievement.
        /// </summary>
        public DateTime DateUnlocked { get; init; }
    }
}
