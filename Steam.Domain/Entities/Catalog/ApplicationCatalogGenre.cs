using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Catalog
{
    public class ApplicationCatalogGenre
    {
        public int ApplicationCatalogId { get; set; }
        public ApplicationCatalog ApplicationCatalog { get; set; } = default!;

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = default!;
    }
}
