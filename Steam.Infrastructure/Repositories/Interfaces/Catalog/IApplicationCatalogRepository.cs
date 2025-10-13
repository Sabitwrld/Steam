using Steam.Domain.Entities.Catalog;

namespace Steam.Infrastructure.Repositories.Interfaces.Catalog
{
    public interface IApplicationCatalogRepository : IRepository<ApplicationCatalog>
    {
        Task<List<ApplicationCatalog>> GetByReleaseDateAsync(DateTime from, DateTime to);
        Task<(IEnumerable<ApplicationCatalog> Items, int TotalCount)> GetFilteredAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm = null,
            int? genreId = null,
            int? tagId = null);
    }
}
