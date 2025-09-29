using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Orders
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int ApplicationId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
