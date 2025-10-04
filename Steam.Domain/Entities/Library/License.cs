using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.Library
{
    public class License : BaseEntity
    {
        public int UserLibraryId { get; set; }
        public UserLibrary UserLibrary { get; set; } = default!;

        public int ApplicationId { get; set; }
        public ApplicationCatalog Application { get; set; } = default!; // Added for easy access to game info

        public string LicenseType { get; set; } = "Lifetime"; // e.g., "Lifetime", "Subscription"
        public DateTime? ExpirationDate { get; set; }

        // --- NEW PROPERTIES ---

        /// <summary>
        /// Total playtime for this game in minutes.
        /// </summary>
        public int PlaytimeInMinutes { get; set; } = 0;

        /// <summary>
        /// The last time the user played this game. Null if never played.
        /// </summary>
        public DateTime? LastPlayed { get; set; }

        /// <summary>
        /// Allows the user to hide the game from their main library view.
        /// </summary>
        public bool IsHidden { get; set; } = false;
    }
}
