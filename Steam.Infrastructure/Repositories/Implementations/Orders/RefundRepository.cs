using Microsoft.EntityFrameworkCore;
using Steam.Domain.Entities.Orders;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Orders;

namespace Steam.Infrastructure.Repositories.Implementations.Orders
{
    public class RefundRepository : Repository<Refund>, IRefundRepository
    {
        public RefundRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Refund> Items, int TotalCount)> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsNoTracking();
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, totalCount);
        }
    }
}
