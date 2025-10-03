using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Store
{
    public class Campaign : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation Property: A campaign can have multiple discounts
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}
