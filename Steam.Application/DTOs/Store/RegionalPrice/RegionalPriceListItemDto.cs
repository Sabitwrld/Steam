namespace Steam.Application.DTOs.Store.RegionalPrice
{
    public record RegionalPriceListItemDto
    {
        public int Id { get; init; }
        public string Currency { get; init; } = default!;
        public decimal Amount { get; init; }
    }
}
