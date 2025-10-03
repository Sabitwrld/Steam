namespace Steam.Application.DTOs.ReviewsRating.Rating
{
    public record RatingReturnDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int ApplicationId { get; init; }
        public int Score { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
