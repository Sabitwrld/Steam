using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Catalog;

namespace Steam.Infrastructure.Repositories.Implementations.Catalog
{
    public class MediaRepository : Repository<Media>, IMediaRepository
    {
        public MediaRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<List<Media>> GetByApplicationIdAsync(int applicationId)
        {
            return await _dbSet
                         .Where(m => m.ApplicationId == applicationId && !m.IsDeleted)
                         .OrderBy(m => m.Order)
                         .ToListAsync();
        }
    }
}
