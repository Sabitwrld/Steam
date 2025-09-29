using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Orders.CartItem
{
    public record CartItemUpdateDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public int Quantity { get; init; }
    }
}
