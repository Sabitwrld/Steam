using Steam.Application.DTOs.Orders.CartItem;

namespace Steam.Application.DTOs.Orders.Cart
{
    public record CartReturnDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public List<CartItemReturnDto> Items { get; set; } = new(); // Changed to 'set'
        public decimal TotalPrice { get; set; } // Changed to 'set'
    }
}
