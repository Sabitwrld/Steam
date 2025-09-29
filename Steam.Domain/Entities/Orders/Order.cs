using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>(); public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Completed, Canceled
    }
}
