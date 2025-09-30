namespace Steam.Application.DTOs.Catalog.Genre
{
    public record GenreUpdateDto
    {
        public string Name { get; init; } = default!;
    }
}
