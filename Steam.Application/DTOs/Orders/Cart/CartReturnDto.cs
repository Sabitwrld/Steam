using Steam.Application.DTOs.Orders.CartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Orders.Cart
{
    public record CartReturnDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public List<CartItemReturnDto> Items { get; init; } = new();
    }
}
