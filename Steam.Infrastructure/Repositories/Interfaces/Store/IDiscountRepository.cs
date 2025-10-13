using Steam.Domain.Entities.Store;

namespace Steam.Infrastructure.Repositories.Interfaces.Store
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<(IEnumerable<Discount> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);

    }
}
