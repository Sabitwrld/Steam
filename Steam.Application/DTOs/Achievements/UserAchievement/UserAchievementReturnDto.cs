using Steam.Application.DTOs.Achievements.Achievements;

namespace Steam.Application.DTOs.Achievements.UserAchievement
{
    public record UserAchievementReturnDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int AchievementId { get; init; }
        public DateTime DateUnlocked { get; init; }
        public AchievementReturnDto Achievement { get; init; }
    }
}
