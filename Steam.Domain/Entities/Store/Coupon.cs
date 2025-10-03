using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Store
{
    public class Coupon : BaseEntity
    {
        public string Code { get; set; } = default!;
        public decimal DiscountPercent { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
