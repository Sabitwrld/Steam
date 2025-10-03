using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Store
{
    public class RegionalPrice : BaseEntity
    {
        public int PricePointId { get; set; }
        public string Currency { get; set; } = default!; // e.g., "AZN", "USD", "EUR"
        public decimal Amount { get; set; }

        // Navigation Property
        public PricePoint PricePoint { get; set; } = default!;
    }
}
