using Steam.Application.DTOs.Orders.CartItem;

namespace Steam.Application.DTOs.Orders.Cart
{
    public record CartCreateDto
    {
        public string UserId { get; init; } = default!; // CHANGED: from int to string
        public List<CartItemCreateDto> Items { get; init; } = new();
    }
}
