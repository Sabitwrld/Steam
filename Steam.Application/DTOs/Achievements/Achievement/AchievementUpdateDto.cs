namespace Steam.Application.DTOs.Achievements.Achievements
{
    /// <summary>
    /// Represents the data required to update an existing achievement.
    /// Used by administrators to correct or change achievement details.
    /// </summary>
    public record AchievementUpdateDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public int Points { get; init; }
        public string IconUrl { get; init; } = string.Empty;
    }
}
