namespace Steam.Application.DTOs.Catalog.Media
{
    public record MediaListItemDto
    {
        public int Id { get; init; }
        public string Url { get; init; } = default!;
        public string MediaType { get; init; } = default!;
        public int Order { get; init; }
    }
}
