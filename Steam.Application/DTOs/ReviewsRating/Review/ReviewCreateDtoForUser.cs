namespace Steam.Application.DTOs.ReviewsRating.Review
{
    public record ReviewCreateDtoForUser
    {
        public int ApplicationId { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Content { get; init; } = string.Empty;
        public bool IsRecommended { get; init; }
    }
}
