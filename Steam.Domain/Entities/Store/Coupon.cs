using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Store
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; } = default!;
        public decimal DiscountPercent { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
