namespace Steam.Application.DTOs.Achievements.Achievements
{
    /// <summary>
    /// Represents the data required to create a new achievement for a specific application.
    /// This would be used by an administrator or developer.
    /// </summary>
    public record AchievementCreateDto
    {
        /// <summary>
        /// The ID of the application this achievement belongs to.
        /// </summary>
        public int ApplicationId { get; init; }

        /// <summary>
        /// The name of the achievement (e.g., "Headshot Master").
        /// </summary>
        public string Name { get; init; } = default!;

        /// <summary>
        /// A detailed description of how to unlock the achievement.
        /// </summary>
        public string Description { get; init; } = default!;

        /// <summary>
        /// Points awarded to the user upon unlocking (optional gamification element).
        /// </summary>
        public int Points { get; init; }

        /// <summary>
        /// URL to the achievement's icon (locked or unlocked version).
        /// </summary>
        public string IconUrl { get; init; } = string.Empty;
    }
}
