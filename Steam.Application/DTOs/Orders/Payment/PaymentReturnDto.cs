namespace Steam.Application.DTOs.Orders.Payment
{
    public record PaymentReturnDto
    {
        public int Id { get; init; }
        public int OrderId { get; init; }
        public string Method { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public decimal Amount { get; init; }
        public string? TransactionId { get; init; }
        public DateTime? PaymentDate { get; init; }
    }
}
