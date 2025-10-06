namespace Steam.Application.DTOs.Achievements.Badge
{
    public record BadgeUpdateDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string IconUrl { get; init; } = string.Empty;
    }
}
