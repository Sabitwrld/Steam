using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;
using Steam.Domain.Entities.Identity;

namespace Steam.Domain.Entities.Store
{
    public class Wishlist : BaseEntity
    {
        public string UserId { get; set; } = default!; // CHANGED: from int to string
        public int ApplicationId { get; set; }

        // Navigation Properties
        public AppUser User { get; set; } = default!; // ADD THIS
        public ApplicationCatalog Application { get; set; } = default!;
    }
}
