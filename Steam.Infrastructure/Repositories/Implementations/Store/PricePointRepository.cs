using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Store;

namespace Steam.Infrastructure.Repositories.Implementations.Store
{
    public class PricePointRepository : Repository<PricePoint>, IPricePointRepository
    {
        public PricePointRepository(AppDbContext context) : base(context)
        {
        }
    }
}
