namespace Steam.Application.DTOs.Store.PricePoint
{
    public record PricePointListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public decimal BasePrice { get; init; }
    }
}
