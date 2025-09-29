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
        public double Percentage { get; set; }// % endirim 
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

    }
}
