using Steam.Domain.Entities.Orders;

namespace Steam.Infrastructure.Repositories.Interfaces.Orders
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart?> GetByUserIdWithItemsAsync(string userId);

    }
}
