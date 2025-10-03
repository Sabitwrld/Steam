namespace Steam.Application.DTOs.Orders.Refund
{
    public record RefundListItemDto
    {
        public int Id { get; init; }
        public int PaymentId { get; init; }
        public string Reason { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
    }
}
