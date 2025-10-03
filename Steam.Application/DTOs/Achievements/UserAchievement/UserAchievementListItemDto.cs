namespace Steam.Application.DTOs.Achievements.UserAchievement
{
    public record UserAchievementListItemDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int AchievementId { get; init; }
        public DateTime DateUnlocked { get; init; }
    }
}
