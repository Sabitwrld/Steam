using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Wishlist
{
    public record WishlistUpdateDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public List<int> ApplicationIds { get; init; } = new();
    }
}
