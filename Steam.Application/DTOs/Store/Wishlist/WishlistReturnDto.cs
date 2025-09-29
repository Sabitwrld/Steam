using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Wishlist
{
    public record WishlistReturnDto 
    { 
        public int Id { get; init; } 
        public int UserId { get; init; } 
        public int ApplicationId { get; init; } 
        public DateTime AddedDate { get; init; } 
    }
}
