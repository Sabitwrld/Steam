namespace Steam.Application.DTOs.Orders.Refund
{
    public record RefundUpdateDto
    {
        public int Id { get; init; }
        public string Reason { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
    }
}
