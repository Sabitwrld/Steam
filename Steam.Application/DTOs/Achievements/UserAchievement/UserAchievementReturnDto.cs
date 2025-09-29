using Steam.Application.DTOs.Achievements.Achievements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
