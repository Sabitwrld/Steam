namespace Steam.Application.DTOs.Store.RegionalPrice
{
    public record RegionalPriceUpdateDto
    {
        public int Id { get; init; }
        public string Currency { get; init; } = default!;
        public decimal Amount { get; init; }
    }
}
