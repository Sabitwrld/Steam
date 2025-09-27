namespace Steam.Application.DTOs.Catalog.Genre
{
    public record GenreCreateDto
    {
        public string Name { get; init; } = default!;
    }
}
