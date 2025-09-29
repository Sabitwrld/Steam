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
        public int PricePointId { get; set; }
        public double Percentage { get; set; } // məsələn 50 → %50 
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        // Navigation
        public PricePoint PricePoint { get; set; } = default!;
    }
}
