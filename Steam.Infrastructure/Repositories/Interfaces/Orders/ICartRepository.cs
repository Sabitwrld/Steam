using Steam.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Repositories.Interfaces.Orders
{
    public interface ICartRepository : IRepository<Cart>
    {
    }
}
