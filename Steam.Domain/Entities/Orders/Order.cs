using Steam.Domain.Entities.Common;
using Steam.Domain.Entities.Identity;

namespace Steam.Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; } = default!; // CHANGED: from int to string
        public AppUser User { get; set; } = default!; // ADDED: Navigation property to user

        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }
    }
}
