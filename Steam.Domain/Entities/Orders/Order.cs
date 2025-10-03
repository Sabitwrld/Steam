using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; } // Added for clarity
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending"; // e.g., Pending, Completed, Canceled

        // Navigation Properties
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; } // A completed order will have one payment
    }
}
