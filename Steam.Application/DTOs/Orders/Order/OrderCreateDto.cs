using Steam.Application.DTOs.Orders.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Orders.Order
{
    public record OrderCreateDto
    {
        public int UserId { get; init; }
        public List<OrderItemCreateDto> Items { get; init; } = new();
    }
}
