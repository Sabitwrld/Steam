using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Catalog
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; } = default!;
        public ICollection<ApplicationCatalog> Applications { get; set; } = new List<ApplicationCatalog>();
    }
}
