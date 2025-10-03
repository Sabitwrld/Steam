namespace Steam.Application.DTOs.Store.PricePoint
{
    public record PricePointCreateDto
    {
        public int ApplicationId { get; init; }
        public string Name { get; init; } = "Default";
        public decimal BasePrice { get; init; }
    }
}
