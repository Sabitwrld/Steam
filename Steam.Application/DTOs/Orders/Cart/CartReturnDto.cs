using Steam.Application.DTOs.Orders.CartItem;

namespace Steam.Application.DTOs.Orders.Cart
{
    public record CartReturnDto
    {
        public int Id { get; init; }
        public string UserId { get; init; } = default!; // CHANGED: from int to string
        public List<CartItemReturnDto> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
    }
}
