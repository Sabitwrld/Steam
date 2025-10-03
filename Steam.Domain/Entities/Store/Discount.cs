using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Store
{
    public class Discount : BaseEntity
    {
        public int ApplicationId { get; set; }
        public int? CampaignId { get; set; } // Optional: Link to a campaign
        public decimal Percent { get; set; } // e.g., 50 for 50%
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation Properties
        public ApplicationCatalog Application { get; set; } = default!;
        public Campaign? Campaign { get; set; }
    }
}
