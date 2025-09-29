using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
