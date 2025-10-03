namespace Steam.Application.DTOs.Orders.OrderItem
{
    public record OrderItemReturnDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public string ApplicationName { get; init; } = default!; // Added for context
        public decimal Price { get; init; }
        public int Quantity { get; init; }
    }
}
