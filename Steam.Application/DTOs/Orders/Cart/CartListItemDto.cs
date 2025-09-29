using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Orders.Cart
{
    public record CartListItemDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int ItemCount { get; init; }
    }
}
