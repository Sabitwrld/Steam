using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;
using Steam.Domain.Entities.Identity;

namespace Steam.Domain.Entities.Store
{
    public class Gift : BaseEntity
    {
        public string SenderId { get; set; } = default!;   // CHANGED
        public string ReceiverId { get; set; } = default!; // CHANGED
        public int ApplicationId { get; set; }
        public System.DateTime SentAt { get; set; }
        public bool IsRedeemed { get; set; }

        public AppUser Sender { get; set; } = default!;   // ADDED
        public AppUser Receiver { get; set; } = default!; // ADDED
        public ApplicationCatalog Application { get; set; } = default!;
    }
}
