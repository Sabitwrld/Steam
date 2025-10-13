using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Catalog;

namespace Steam.Infrastructure.Repositories.Implementations.Catalog
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<(IEnumerable<Tag> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsNoTracking();
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }
    }
}
