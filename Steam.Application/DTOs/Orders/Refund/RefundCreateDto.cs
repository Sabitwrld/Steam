namespace Steam.Application.DTOs.Orders.Refund
{
    public record RefundCreateDto
    {
        public int PaymentId { get; init; }
        public string Reason { get; init; } = string.Empty;
        public decimal Amount { get; init; } // Specify the amount to be refunded
    }
}
