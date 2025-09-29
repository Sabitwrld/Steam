using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Orders.OrderItem
{
    public record OrderItemReturnDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
    }
}
