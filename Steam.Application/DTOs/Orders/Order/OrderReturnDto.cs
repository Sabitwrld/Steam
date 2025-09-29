using Steam.Application.DTOs.Orders.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Orders.Order
{
    public record OrderReturnDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public decimal TotalPrice { get; init; }
        public string Status { get; init; } = string.Empty;
        public List<OrderItemReturnDto> Items { get; init; } = new();
    }
}
