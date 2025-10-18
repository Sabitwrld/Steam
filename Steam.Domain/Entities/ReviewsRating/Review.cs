using Steam.Domain.Entities.Catalog;
using Steam.Domain.Entities.Common;
using Steam.Domain.Entities.Identity;

namespace Steam.Domain.Entities.ReviewsRating
{
    public class Review : BaseEntity
    {
        // Foreign Keys
        public string UserId { get; set; } = default!; // Changed to string to match AppUser's Id
        public int ApplicationId { get; set; }

        // Review Content
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Represents the user's recommendation. True for "Recommended", False for "Not Recommended".
        /// This replaces the separate Rating/Score entity.
        /// </summary>
        public bool IsRecommended { get; set; }
        public bool IsApproved { get; set; } = false; // Varsayılan olaraq təsdiqlənməmiş olsun

        // Community Feedback
        public int HelpfulCount { get; set; } = 0;
        public int FunnyCount { get; set; } = 0;

        // Navigation Properties
        public AppUser User { get; set; } = default!;
        public ApplicationCatalog Application { get; set; } = default!;
    }
}
