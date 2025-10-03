using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Store
{
    public class PricePoint : BaseEntity
    {
        public int ApplicationId { get; set; }
        public string Name { get; set; } = "Default"; // e.g., "Standard Edition", "Deluxe Edition"
        public decimal BasePrice { get; set; } // The base price, e.g., in USD

        // Navigation Properties
        public ApplicationCatalog Application { get; set; } = default!;
        public ICollection<RegionalPrice> RegionalPrices { get; set; } = new List<RegionalPrice>();
    }
}
