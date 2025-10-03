namespace Steam.Application.DTOs.Orders.CartItem
{
    public record CartItemCreateDto
    {
        public int ApplicationId { get; init; }
        public int Quantity { get; init; }
    }
}
