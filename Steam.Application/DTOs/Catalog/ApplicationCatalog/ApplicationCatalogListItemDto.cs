namespace Steam.Application.DTOs.Catalog.Application
{
    public record ApplicationCatalogListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
        public string Developer { get; init; } = default!;
        public string Publisher { get; init; } = default!;
        public string ApplicationType { get; init; } = default!;
    }
}
