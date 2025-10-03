using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Achievements
{
    public class UserAchievement : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AchievementId { get; set; }
        public DateTime DateUnlocked { get; set; }

        public Achievement Achievement { get; set; } = null!;
    }
}
