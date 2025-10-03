namespace Steam.Application.DTOs.ReviewsRating.Rating
{
    public record RatingUpdateDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int ApplicationId { get; init; }
        public int Score { get; init; } // 1-10
    }
}
