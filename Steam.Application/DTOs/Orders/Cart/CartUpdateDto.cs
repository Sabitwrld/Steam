using Steam.Application.DTOs.Orders.CartItem;

namespace Steam.Application.DTOs.Orders.Cart
{
    public record CartUpdateDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public List<CartItemUpdateDto> Items { get; init; } = new();
    }
}
