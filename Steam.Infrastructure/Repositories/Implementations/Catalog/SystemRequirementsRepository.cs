using Steam.Domain.Entities.Catalog;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Catalog;

namespace Steam.Infrastructure.Repositories.Implementations.Catalog
{
    public class SystemRequirementsRepository : Repository<SystemRequirements>, ISystemRequirementsRepository
    {
        public SystemRequirementsRepository(AppDbContext context) : base(context)
        {
        }
    }
}
