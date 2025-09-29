using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Achievements.Achievements
{
    public record AchievementCreateDto
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public int Points { get; init; }
    }
}
