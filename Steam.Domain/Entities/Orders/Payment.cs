using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Orders
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string Status { get; set; } = "Pending"; // Pending, Paid, Failed
        public decimal Amount { get; set; }
    }
}
