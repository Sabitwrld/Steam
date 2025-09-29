using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Achievements
{
    public class Leaderboard : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
    }
}
