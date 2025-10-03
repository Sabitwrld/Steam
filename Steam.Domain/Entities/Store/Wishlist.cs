using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Store
{
    public class Wishlist : BaseEntity
    {
        public int UserId { get; set; }
        public int ApplicationId { get; set; }

        public ApplicationCatalog Application { get; set; } = default!;
    }
}
