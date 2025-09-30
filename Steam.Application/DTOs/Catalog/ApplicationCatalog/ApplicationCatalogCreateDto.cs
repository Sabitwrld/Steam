namespace Steam.Application.DTOs.Catalog.Application
{
    public record ApplicationCatalogCreateDto
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public DateTime ReleaseDate { get; init; }
        public string Developer { get; init; } = default!;
        public string Publisher { get; init; } = default!;
        public string ApplicationType { get; init; } = default!;
        public List<int>? GenreIds { get; init; }
        public List<int>? TagIds { get; init; }
    }
}
