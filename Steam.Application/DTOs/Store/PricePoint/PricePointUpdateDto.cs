namespace Steam.Application.DTOs.Store.PricePoint
{
    public record PricePointUpdateDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = "Default";
        public decimal BasePrice { get; init; }
    }
}
