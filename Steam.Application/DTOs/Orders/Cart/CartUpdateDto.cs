using Steam.Application.DTOs.Orders.CartItem;

namespace Steam.Application.DTOs.Orders.Cart
{
    public record CartUpdateDto
    {
        public int Id { get; init; }
        public string UserId { get; init; } = default!; // CHANGED: from int to string
        public List<CartItemUpdateDto> Items { get; init; } = new();
    }
}
