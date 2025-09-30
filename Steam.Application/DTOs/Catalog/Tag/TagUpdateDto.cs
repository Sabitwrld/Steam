namespace Steam.Application.DTOs.Catalog.Tag
{
    public record TagUpdateDto
    {
        public string Name { get; init; } = default!;
    }
}
