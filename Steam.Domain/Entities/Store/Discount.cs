using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Store
{
    public class Discount : BaseEntity
    {
        public int ApplicationId { get; set; }
        public decimal Percent { get; set; } // e.g., 50 = 50%
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ApplicationCatalog Application { get; set; } = default!;
    }
}
