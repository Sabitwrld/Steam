using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Store;

namespace Steam.Infrastructure.Repositories.Implementations.Store
{
    public class GiftRepository : Repository<Gift>, IGiftRepository
    {
        public GiftRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<(IEnumerable<Gift> Items, int TotalCount)> GetByUserIdPagedAsync(string userId, int pageNumber, int pageSize)
        {
            var query = _dbSet.Where(g => g.ReceiverId == userId).AsNoTracking();
            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(g => g.SentAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (items, totalCount);
        }
    }
}
