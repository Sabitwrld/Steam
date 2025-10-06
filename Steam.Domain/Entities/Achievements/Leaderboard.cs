using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;
using Steam.Domain.Entities.Identity;

namespace Steam.Domain.Entities.Achievements
{
    public class Leaderboard : BaseEntity
    {
        public int ApplicationId { get; set; }
        public string UserId { get; set; } = default!;
        public int Score { get; set; }

        public ApplicationCatalog Application { get; set; } = default!;
        public AppUser User { get; set; } = default!;
    }
}
