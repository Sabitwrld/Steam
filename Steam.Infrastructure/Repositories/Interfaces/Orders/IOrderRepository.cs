using Steam.Domain.Entities.Orders;

namespace Steam.Infrastructure.Repositories.Interfaces.Orders
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<(IEnumerable<Order> Items, int TotalCount)> GetByUserIdPagedAsync(
           string userId,
           int pageNumber,
           int pageSize);
    }
}
