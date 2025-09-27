namespace Steam.Application.DTOs.Catalog.Application
{
    public record ApplicationCatalogReturnDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public DateTime ReleaseDate { get; init; }
        public string Developer { get; init; } = default!;
        public string Publisher { get; init; } = default!;
        public string ApplicationType { get; init; } = default!;
        public List<string> Genres { get; init; } = new();
        public List<string> Tags { get; init; } = new();
        public List<string> MediaUrls { get; init; } = new();
    }
}
