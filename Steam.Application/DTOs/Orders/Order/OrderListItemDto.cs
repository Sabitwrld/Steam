namespace Steam.Application.DTOs.Orders.Order
{
    public record OrderListItemDto
    {
        public int Id { get; init; }
        public DateTime OrderDate { get; init; }
        public int ItemCount { get; init; }
        public decimal TotalPrice { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
