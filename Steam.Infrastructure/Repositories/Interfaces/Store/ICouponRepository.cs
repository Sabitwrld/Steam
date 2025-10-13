using Steam.Domain.Entities.Store;

namespace Steam.Infrastructure.Repositories.Interfaces.Store
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        Task<(IEnumerable<Coupon> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);

    }
}
