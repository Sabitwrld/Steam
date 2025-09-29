using Steam.Domain.Entities.Library;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Repositories.Implementations.Library
{
    public class LicenseRepository : Repository<License>, ILicenseRepository
    {
        public LicenseRepository(AppDbContext context) : base(context)
        {
        }
    }
}
