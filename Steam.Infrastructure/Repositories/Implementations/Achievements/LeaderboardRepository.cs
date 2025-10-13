using Microsoft.EntityFrameworkCore;
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
        public async Task<(IEnumerable<Leaderboard> Items, int TotalCount)> GetByApplicationIdPagedAsync(int applicationId, int pageNumber, int pageSize)
        {
            var query = _dbSet
                .Where(l => l.ApplicationId == applicationId)
                .Include(l => l.User)
                .OrderByDescending(l => l.Score)
                .AsNoTracking();

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }
    }
}
