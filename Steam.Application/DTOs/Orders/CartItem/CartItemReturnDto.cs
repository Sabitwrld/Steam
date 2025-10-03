namespace Steam.Application.DTOs.Orders.CartItem
{
    public record CartItemReturnDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public string ApplicationName { get; init; } = default!;
        public int Quantity { get; init; }
        public decimal Price { get; set; } // Changed to 'set'
        public decimal TotalPrice => Price * Quantity;
    }
}
