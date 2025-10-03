namespace Steam.Application.DTOs.Orders.OrderItem
{
    public record OrderItemUpdateDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public decimal Price { get; init; }
        public int Quantity { get; init; }
    }
}
