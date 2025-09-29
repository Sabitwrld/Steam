using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Store
{
    public class Voucher : BaseEntity
    {
        public string Code { get; set; } = default!; 
        public int ApplicationId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsRedeemed { get; set; }
    }
}
