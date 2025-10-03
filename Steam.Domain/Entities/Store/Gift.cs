using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Domain.Entities.Store
{
    public class Gift : BaseEntity
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int ApplicationId { get; set; }
        public DateTime SentAt { get; set; }

        public ApplicationCatalog Application { get; set; } = default!;
    }
}
