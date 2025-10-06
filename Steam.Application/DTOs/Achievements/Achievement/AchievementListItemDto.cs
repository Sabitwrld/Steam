namespace Steam.Application.DTOs.Achievements.Achievements
{
    /// <summary>
    /// Represents a summary of an achievement, suitable for displaying in a list.
    /// </summary>
    public record AchievementListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string IconUrl { get; init; } = string.Empty;
    }
}
