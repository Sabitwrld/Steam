using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Store;

namespace Steam.Infrastructure.Repositories.Implementations.Store
{
    public class PricePointRepository : Repository<PricePoint>, IPricePointRepository
    {
        public PricePointRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<(IEnumerable<PricePoint> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsNoTracking();
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }

        public async Task<PricePoint?> GetByIdWithRegionsAsync(int id)
        {
            return await _dbSet
                .Include(p => p.RegionalPrices)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
