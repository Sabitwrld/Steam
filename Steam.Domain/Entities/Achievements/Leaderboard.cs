using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Achievements
{
    public class Leaderboard : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
    }
}
