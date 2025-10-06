using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Achievements
{
    public class Achievement : BaseEntity
    {
        public int ApplicationId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Points { get; set; }
        public string IconUrl { get; set; } = string.Empty;

        public ApplicationCatalog Application { get; set; } = default!;
    }
}
