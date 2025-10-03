namespace Steam.Application.DTOs.Achievements.UserAchievement
{
    public record UserAchievementCreateDto
    {
        public int UserId { get; init; }
        public int AchievementId { get; init; }
    }
}
