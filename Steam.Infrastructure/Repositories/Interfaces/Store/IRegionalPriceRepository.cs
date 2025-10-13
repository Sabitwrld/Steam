using Steam.Domain.Entities.Store;

namespace Steam.Infrastructure.Repositories.Interfaces.Store
{
    public interface IRegionalPriceRepository : IRepository<RegionalPrice>
    {
        Task<(IEnumerable<RegionalPrice> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);

    }
}
