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
        public int ApplicationId { get; set; }
        public string Region { get; set; } = default!; // e.g., "AZN", "USD", "EUR"
        public decimal Amount { get; set; }

        public ApplicationCatalog Application { get; set; } = default!;
    }
}
