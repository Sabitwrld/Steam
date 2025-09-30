using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Catalog
{
    public class ApplicationCatalogTag
    {
        public int ApplicationCatalogId { get; set; }
        public ApplicationCatalog ApplicationCatalog { get; set; } = default!;

        public int TagId { get; set; }
        public Tag Tag { get; set; } = default!;
    }
}
