namespace Steam.Application.DTOs.Achievements.Badge
{
    public record BadgeCreateDto
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
