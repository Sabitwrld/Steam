using Steam.Domain.Entities.Common;

namespace Steam.Domain.Entities.ReviewsRating
{
    public class Rating : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        public int Score { get; set; } // 1-10
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
