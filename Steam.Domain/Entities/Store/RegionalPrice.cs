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
        public string Currency { get; set; } = default!; // AZN, USD, EUR
        public decimal Amount { get; set; } 
        // Navigation
        public PricePoint PricePoint { get; set; } = default!;
    }
}
