using Steam.Application.DTOs.Orders.OrderItem;

namespace Steam.Application.DTOs.Orders.Order
{
    public record OrderUpdateDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public List<OrderItemUpdateDto> Items { get; init; } = new();
        public string Status { get; init; } = string.Empty;
    }
}
