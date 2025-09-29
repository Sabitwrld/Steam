using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Orders
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
