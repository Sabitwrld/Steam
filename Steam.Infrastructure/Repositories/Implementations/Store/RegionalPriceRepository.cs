using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Store;

namespace Steam.Infrastructure.Repositories.Implementations.Store
{
    public class RegionalPriceRepository : Repository<RegionalPrice>, IRegionalPriceRepository
    {
        public RegionalPriceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
