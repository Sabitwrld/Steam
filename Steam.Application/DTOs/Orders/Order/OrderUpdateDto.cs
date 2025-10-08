using Steam.Application.DTOs.Orders.OrderItem;

namespace Steam.Application.DTOs.Orders.Order
{
    public record OrderUpdateDto
    {
        public int Id { get; init; }
        public string UserId { get; init; } = default!; // CHANGED
        public List<OrderItemUpdateDto> Items { get; init; } = new();
        public string Status { get; init; } = string.Empty;
    }
}
