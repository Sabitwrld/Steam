using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Achievements.UserAchievement
{
    public record UserAchievementCreateDto
    {
        public int UserId { get; init; }
        public int AchievementId { get; init; }
    }
}
