namespace Steam.Application.DTOs.Achievements.Achievements
{
    /// <summary>
    /// Represents the full details of an achievement when returned from the API.
    /// </summary>
    public record AchievementReturnDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public int Points { get; init; }
        public string IconUrl { get; init; } = string.Empty;
    }
}
