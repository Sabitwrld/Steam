using Steam.Domain.Entities.Store;
using Steam.Infrastructure.Persistence;
using Steam.Infrastructure.Repositories.Interfaces.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Infrastructure.Repositories.Implementations.Store
{
    public class VoucherRepository : Repository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(AppDbContext context) : base(context)
        {
        }
    }
}
