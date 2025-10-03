namespace Steam.Application.DTOs.Achievements.Achievements
{
    public record AchievementUpdateDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int Points { get; init; }
    }
}
