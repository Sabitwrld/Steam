using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Library
{
    public class License : BaseEntity
    {
        public int UserLibraryId { get; set; }
        public UserLibrary UserLibrary { get; set; } = default!;

        public int ApplicationId { get; set; }  // Oyunun Id-si
        public string LicenseType { get; set; } = default!; // "Lifetime", "Subscription"
        public DateTime? ExpirationDate { get; set; }
    }
}
