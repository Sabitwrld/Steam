using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Catalog;

namespace Steam.Infrastructure.Repositories.Implementations.Catalog
{
    public class ApplicationCatalogRepository : Repository<ApplicationCatalog>, IApplicationCatalogRepository
    {
        public ApplicationCatalogRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<ApplicationCatalog>> GetByReleaseDateAsync(DateTime from, DateTime to)
        {
            return await _dbSet
                .Where(a => a.ReleaseDate >= from && a.ReleaseDate <= to && !a.IsDeleted)
                .Include(a => a.Genres)
                .Include(a => a.Tags)
                .Include(a => a.Media)
                .Include(a => a.SystemRequirements)
                .ToListAsync();
        }
    }
}
