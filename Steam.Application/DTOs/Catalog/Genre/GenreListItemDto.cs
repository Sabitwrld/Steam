namespace Steam.Application.DTOs.Catalog.Genre
{
    public record GenreListItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
    }
}
