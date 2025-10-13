using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Achievements;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Achievements;

namespace Steam.Infrastructure.Repositories.Implementations.Achievements
{
    public class AchievementRepository : Repository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<(IEnumerable<Achievement> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsNoTracking();
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }
    }
}
