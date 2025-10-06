namespace Steam.Application.DTOs.Achievements.Badge
{
    /// <summary>
    /// Represents the data needed to create a new badge.
    /// </summary>
    public record BadgeCreateDto
    {
        public int ApplicationId { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string IconUrl { get; init; } = string.Empty;
    }
}
