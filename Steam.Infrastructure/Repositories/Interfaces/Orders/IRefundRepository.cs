using Steam.Domain.Entities.Orders;

namespace Steam.Infrastructure.Repositories.Interfaces.Orders
{
    public interface IRefundRepository : IRepository<Refund>
    {
        Task<(IEnumerable<Refund> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);

    }
}
