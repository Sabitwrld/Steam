using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Wishlist
{
    public record WishlistCreateDto 
    { 
        public int UserId { get; init; } 
        public int ApplicationId { get; init; }
    }
}
