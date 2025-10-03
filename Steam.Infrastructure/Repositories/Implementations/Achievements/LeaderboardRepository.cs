using Steam.Domain.Entities.Achievements;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Achievements;

namespace Steam.Infrastructure.Repositories.Implementations.Achievements
{
    public class LeaderboardRepository : Repository<Leaderboard>, ILeaderboardRepository
    {
        public LeaderboardRepository(AppDbContext context) : base(context)
        {
        }
    }
}
