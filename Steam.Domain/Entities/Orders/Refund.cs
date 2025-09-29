using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Orders
{
    public class Refund : BaseEntity
    {
        public int PaymentId { get; set; }
        public Payment Payment { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public string Status { get; set; } = "Requested"; // Requested, Approved, Rejected
    }
}
