using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Store;

namespace Steam.Infrastructure.Repositories.Implementations.Store
{
    public class CampaignRepository : Repository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<(IEnumerable<Campaign> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsNoTracking();
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }
    }
}
