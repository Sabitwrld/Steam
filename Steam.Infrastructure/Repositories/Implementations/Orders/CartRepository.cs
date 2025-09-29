using Steam.Domain.Entities.Orders;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Repositories.Implementations.Orders
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }
    }
}
