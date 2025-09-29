using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces;
using Steam.Infrastructure.Repositories.Interfaces.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Repositories.Implementations.Store
{
    public class PricePointRepository : Repository<PricePoint>, IPricePointRepository
    {
        public PricePointRepository(AppDbContext context) : base(context)
        {
        }
    }
}
