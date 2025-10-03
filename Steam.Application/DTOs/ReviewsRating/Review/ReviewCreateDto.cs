namespace Steam.Application.DTOs.ReviewsRating.Review
{
    public record ReviewCreateDto
    {
        public int UserId { get; init; }
        public int ApplicationId { get; init; }
        public string Content { get; init; } = string.Empty;
    }
}
