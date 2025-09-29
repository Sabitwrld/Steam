using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Achievements.Badge
{
    public record BadgeListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }
}
