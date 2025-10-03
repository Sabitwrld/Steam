using Steam.Application.DTOs.Orders.OrderItem;

namespace Steam.Application.DTOs.Orders.Order
{
    public record OrderCreateDto
    {
        public int UserId { get; init; }
    }
}
