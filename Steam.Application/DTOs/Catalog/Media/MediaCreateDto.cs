namespace Steam.Application.DTOs.Catalog.Media
{
    public record MediaCreateDto
    {
        public int ApplicationId { get; init; }
        public string Url { get; init; } = default!;
        public string MediaType { get; init; } = default!;
        public int Order { get; init; }
    }
}
