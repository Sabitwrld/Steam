using Steam.Domain.Entities.Catalog;

namespace Steam.Infrastructure.Repositories.Interfaces.Catalog
{
    public interface IApplicationCatalogRepository : IRepository<ApplicationCatalog>
    {
        Task<List<ApplicationCatalog>> GetByReleaseDateAsync(DateTime from, DateTime to);
    }
}
