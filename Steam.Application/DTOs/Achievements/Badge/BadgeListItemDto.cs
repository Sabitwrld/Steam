namespace Steam.Application.DTOs.Achievements.Badge
{
    public record BadgeListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string IconUrl { get; init; } = string.Empty;
    }
}
