using Steam.Application.DTOs.Orders.CartItem;

namespace Steam.Application.DTOs.Orders.Cart
{
    public record CartCreateDto
    {
        public int UserId { get; init; }
        public List<CartItemCreateDto> Items { get; init; } = new();
    }
}
