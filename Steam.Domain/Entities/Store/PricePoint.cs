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
        public decimal BasePrice { get; set; } // əsas qiymət (məs. USD-də)
        public ICollection<RegionalPrice> RegionalPrices { get; set; } = new List<RegionalPrice>(); 
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}
