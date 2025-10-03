namespace Steam.Application.DTOs.Achievements.Badge
{
    public record BadgeReturnDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
