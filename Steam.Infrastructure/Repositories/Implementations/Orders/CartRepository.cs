using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Orders;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Orders;

namespace Steam.Infrastructure.Repositories.Implementations.Orders
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Cart?> GetByUserIdWithItemsAsync(string userId)
        {
            // Servisdəki mürəkkəb sorğu məntiqi bura köçürüldü
            return await _dbSet
                .Include(c => c.Items)
                .ThenInclude(ci => ci.Application)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
