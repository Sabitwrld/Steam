using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Store.Wishlist
{
    public record WishlistCreateDtoForUser
    {
        public int ApplicationId { get; init; }
    }
}
