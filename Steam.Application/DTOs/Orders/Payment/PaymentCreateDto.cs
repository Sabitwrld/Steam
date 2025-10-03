namespace Steam.Application.DTOs.Orders.Payment
{
    public record PaymentCreateDto
    {
        public int OrderId { get; init; }
        public string Method { get; init; } = string.Empty; // e.g., "Card", "PayPal"
        // Amount will be calculated from the order in the service layer.
    }
}
