using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Achievements;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Achievements;

namespace Steam.Infrastructure.Repositories.Implementations.Achievements
{
    public class UserAchievementRepository : Repository<UserAchievement>, IUserAchievementRepository
    {
        public UserAchievementRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<(IEnumerable<UserAchievement> Items, int TotalCount)> GetByUserIdPagedAsync(string userId, int pageNumber, int pageSize)
        {
            var query = _dbSet
                .Where(ua => ua.UserId == userId)
                .Include(ua => ua.Achievement)
                .OrderByDescending(ua => ua.DateUnlocked)
                .AsNoTracking();

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }
    }
}
