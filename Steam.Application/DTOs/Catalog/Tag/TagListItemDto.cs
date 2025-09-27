namespace Steam.Application.DTOs.Catalog.Tag
{
    public record TagListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
    }
}
