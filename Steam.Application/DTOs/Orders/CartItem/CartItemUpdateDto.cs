namespace Steam.Application.DTOs.Orders.CartItem
{
    public record CartItemUpdateDto
    {
        public int Quantity { get; init; }
    }
}
