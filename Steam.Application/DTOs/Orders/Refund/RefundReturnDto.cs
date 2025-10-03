namespace Steam.Application.DTOs.Orders.Refund
{
    public record RefundReturnDto
    {
        public int Id { get; init; }
        public int PaymentId { get; init; }
        public string Reason { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public decimal Amount { get; init; }
        public DateTime? RefundDate { get; init; }
    }
}
