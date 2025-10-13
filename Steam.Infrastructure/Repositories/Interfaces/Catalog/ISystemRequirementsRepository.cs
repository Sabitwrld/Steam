using Steam.Domain.Entities.Catalog;

namespace Steam.Infrastructure.Repositories.Interfaces.Catalog
{
    public interface ISystemRequirementsRepository : IRepository<SystemRequirements>
    {
        Task<(IEnumerable<SystemRequirements> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);
    }
}
