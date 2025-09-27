using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Catalog
{
    public class ApplicationCatalog : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime ReleaseDate { get; set; }
        public string Developer { get; set; } = default!;
        public string Publisher { get; set; } = default!;
        public string ApplicationType { get; set; } = default!;


        public ICollection<Media> Media { get; set; } = new List<Media>();
        public ICollection<SystemRequirements> SystemRequirements { get; set; } = new List<SystemRequirements>();
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
