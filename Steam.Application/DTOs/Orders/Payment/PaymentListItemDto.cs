namespace Steam.Application.DTOs.Orders.Payment
{
    public record PaymentListItemDto
    {
        public int Id { get; init; }
        public int OrderId { get; init; }
        public string Method { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public decimal Amount { get; init; }
    }
}
