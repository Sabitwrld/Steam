namespace Steam.Application.DTOs.Store.RegionalPrice
{
    public record RegionalPriceCreateDto
    {
        public int PricePointId { get; init; }
        public string Currency { get; init; } = default!;
        public decimal Amount { get; init; }
    }
}
