using Steam.Domain.Entities.Common;
using Steam.Domain.Entities.Identity;

namespace Steam.Domain.Entities.Achievements
{
    public class UserAchievement : BaseEntity
    {
        public string UserId { get; set; } = default!;
        public int AchievementId { get; set; }
        public DateTime DateUnlocked { get; set; }

        public AppUser User { get; set; } = default!;
        public Achievement Achievement { get; set; } = null!;
    }
}
