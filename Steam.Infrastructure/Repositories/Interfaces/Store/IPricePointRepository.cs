using Steam.Domain.Entities.Store;

namespace Steam.Infrastructure.Repositories.Interfaces.Store
{
    public interface IPricePointRepository : IRepository<PricePoint>
    {
        Task<(IEnumerable<PricePoint> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);
        Task<PricePoint?> GetByIdWithRegionsAsync(int id);
    }
}
