namespace Steam.Application.DTOs.Orders.OrderItem
{
    public record OrderItemCreateDto
    {
        public int ApplicationId { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
    }
}
