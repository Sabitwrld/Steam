namespace Steam.Application.DTOs.Catalog.Genre
{
    public record GenreReturnDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = default!;
    }
}
