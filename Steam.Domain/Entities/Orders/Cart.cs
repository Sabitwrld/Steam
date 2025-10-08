using Steam.Domain.Entities.Common;
using Steam.Domain.Entities.Identity;

namespace Steam.Domain.Entities.Orders
{
    public class Cart : BaseEntity
    {
        public string UserId { get; set; } = default!; // CHANGED: from int to string
        public AppUser User { get; set; } = default!;   // ADDED: Navigation property to user

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
