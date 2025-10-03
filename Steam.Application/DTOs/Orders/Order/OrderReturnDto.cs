using Steam.Application.DTOs.Orders.OrderItem;

namespace Steam.Application.DTOs.Orders.Order
{
    public record OrderReturnDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public DateTime OrderDate { get; init; }
        public decimal TotalPrice { get; init; }
        public string Status { get; init; } = string.Empty;
        public List<OrderItemReturnDto> Items { get; init; } = new();
    }
}
