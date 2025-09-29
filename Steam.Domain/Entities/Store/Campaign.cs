using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Store
{
    public class Campaign : BaseEntity
    {
        public string Name { get; set; } = default!; 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        // Navigations
        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    }
}
