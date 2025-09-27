namespace Steam.Application.DTOs.Catalog.Tag
{
    public record TagUpdateDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
    }
}
