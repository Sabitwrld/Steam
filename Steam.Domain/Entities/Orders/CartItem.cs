using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Orders
{
    public class CartItem : BaseEntity
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        public int ApplicationId { get; set; }
        public int Quantity { get; set; }

        // Navigation property to easily access product details
        public ApplicationCatalog Application { get; set; } = default!;
    }
}
