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

        public async Task<(IEnumerable<ApplicationCatalog> Items, int TotalCount)> GetFilteredAsync(
            int pageNumber, int pageSize, string? searchTerm = null, int? genreId = null, int? tagId = null)
        {
            // `GetQuery` artıq olmadığı üçün birbaşa `_dbSet` (DbSet<ApplicationCatalog>) istifadə edirik
            IQueryable<ApplicationCatalog> query = _dbSet.AsNoTracking();

            // Filtrləmə məntiqi
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a =>
                    a.Name.Contains(searchTerm) ||
                    a.Developer.Contains(searchTerm) ||
                    a.Publisher.Contains(searchTerm));
            }

            if (genreId.HasValue)
            {
                // `Include` etmədən də əlaqəli cədvəl üzərindən filtr vermək mümkündür
                query = query.Where(a => a.Genres.Any(g => g.Id == genreId.Value));
            }

            if (tagId.HasValue)
            {
                query = query.Where(a => a.Tags.Any(t => t.Id == tagId.Value));
            }

            // Filtrlənmiş nəticələrin ümumi sayını hesablamaq (səhifələmədən əvvəl)
            var totalCount = await query.CountAsync();

            // Səhifələməni tətbiq etmək
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
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
