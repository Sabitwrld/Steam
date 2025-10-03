using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Orders
{
    public class Refund : BaseEntity
    {
        public int PaymentId { get; set; }
        public Payment Payment { get; set; } = null!;

        public string Reason { get; set; } = null!;
        public string Status { get; set; } = "Requested"; // e.g., Requested, Approved, Rejected
        public decimal Amount { get; set; } // Added for partial refund capability
        public DateTime? RefundDate { get; set; } // Added for clarity
    }
}
