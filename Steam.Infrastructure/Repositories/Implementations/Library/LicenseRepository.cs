using Steam.Domain.Entities.Library;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Library;

namespace Steam.Infrastructure.Repositories.Implementations.Library
{
    public class LicenseRepository : Repository<License>, ILicenseRepository
    {
        public LicenseRepository(AppDbContext context) : base(context)
        {
        }
    }
}
