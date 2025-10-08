namespace Steam.Application.DTOs.Orders.Cart
{
    public record CartListItemDto
    {
        public int Id { get; init; }
        public string UserId { get; init; } = default!; // CHANGED
        public int ItemCount { get; init; }
    }
}
