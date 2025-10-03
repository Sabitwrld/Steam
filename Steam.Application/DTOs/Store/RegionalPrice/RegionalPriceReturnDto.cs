namespace Steam.Application.DTOs.Store.RegionalPrice
{
    public record RegionalPriceReturnDto
    {
        public int Id { get; init; }
        public int PricePointId { get; init; }
        public string Currency { get; init; } = default!;
        public decimal Amount { get; init; }
    }
}
