namespace Steam.Application.DTOs.Achievements.Badge
{
    public record BadgeUpdateDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
