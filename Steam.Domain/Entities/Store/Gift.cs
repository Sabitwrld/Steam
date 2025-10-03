using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Store
{
    public class Gift : BaseEntity
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int ApplicationId { get; set; }
        public DateTime SentAt { get; set; }

        public bool IsRedeemed { get; set; } // This property was missing

        public ApplicationCatalog Application { get; set; } = default!;
    }
}
