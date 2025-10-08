namespace Steam.Application.DTOs.Orders.Order
{
    public record OrderCreateDto
    {
        public string UserId { get; init; } = default!; // CHANGED
    }
}
