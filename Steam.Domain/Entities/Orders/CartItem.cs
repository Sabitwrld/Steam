using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Orders
{
    public class CartItem : BaseEntity
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;
        public int ApplicationId { get; set; }
        public int Quantity { get; set; }
    }
}
