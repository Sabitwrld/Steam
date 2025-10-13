using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Orders;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Orders;

namespace Steam.Infrastructure.Repositories.Implementations.Orders
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<(IEnumerable<Order> Items, int TotalCount)> GetByUserIdPagedAsync(
            string userId, int pageNumber, int pageSize)
        {
            var query = _dbSet
                .Where(o => o.UserId == userId)
                .Include(o => o.Items) // Servisdəki Include bura köçürüldü
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(o => o.OrderDate) // Sıralama məntiqi də bura köçürüldü
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
