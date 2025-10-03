using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Orders
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ApplicationId { get; set; }
        public decimal Price { get; set; } // Price at the time of purchase
        public int Quantity { get; set; }

        // Navigation property to easily access product details
        public ApplicationCatalog Application { get; set; } = default!;
    }
}
