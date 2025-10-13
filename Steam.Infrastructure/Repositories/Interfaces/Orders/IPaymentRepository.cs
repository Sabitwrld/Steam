using Steam.Domain.Entities.Orders;

namespace Steam.Infrastructure.Repositories.Interfaces.Orders
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<(IEnumerable<Payment> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize);
    }
}
