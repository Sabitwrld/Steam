using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Store
{
    public class Wishlist : BaseEntity
    {
        public int UserId { get; set; }
        public int ApplicationId { get; set; }

        public ApplicationCatalog Application { get; set; } = default!;
    }
}
