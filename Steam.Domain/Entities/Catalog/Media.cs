using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Catalog
{
    public class Media : BaseEntity
    {
        public int ApplicationId { get; set; }

        public string Url { get; set; } = default!;
        public string MediaType { get; set; } = default!;

        public int Order { get; set; }

        public ApplicationCatalog Application { get; set; } = default!;
    }
}
