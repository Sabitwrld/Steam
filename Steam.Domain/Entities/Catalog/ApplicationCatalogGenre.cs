namespace Steam.Domain.Entities.Catalog
{
    public class ApplicationCatalogGenre
    {
        public int ApplicationCatalogId { get; set; }
        public ApplicationCatalog ApplicationCatalog { get; set; } = default!;

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = default!;
    }
}
