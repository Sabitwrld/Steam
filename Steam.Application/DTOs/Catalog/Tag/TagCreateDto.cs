namespace Steam.Application.DTOs.Catalog.Tag
{
    public record TagCreateDto
    {
        public string Name { get; init; } = default!;
    }
}
