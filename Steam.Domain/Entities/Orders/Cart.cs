using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Orders
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
