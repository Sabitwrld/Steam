using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Orders
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public string Method { get; set; } = null!; // e.g., "Card", "PayPal"
        public string Status { get; set; } = "Pending"; // e.g., Pending, Paid, Failed
        public decimal Amount { get; set; }
        public string? TransactionId { get; set; } // Added for payment provider reference
        public DateTime? PaymentDate { get; set; } // Added for clarity
    }
}
