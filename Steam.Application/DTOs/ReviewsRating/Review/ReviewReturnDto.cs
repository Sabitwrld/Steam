namespace Steam.Application.DTOs.ReviewsRating.Review
{
    public record ReviewReturnDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int ApplicationId { get; init; }
        public string Content { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
    }
}
