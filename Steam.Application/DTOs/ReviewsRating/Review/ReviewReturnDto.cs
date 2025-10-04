namespace Steam.Application.DTOs.ReviewsRating.Review
{
    public record ReviewReturnDto
    {
        public int Id { get; init; }
        public string UserId { get; init; } = default!;
        public string UserName { get; init; } = default!; // Added for display
        public int ApplicationId { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Content { get; init; } = string.Empty;
        public bool IsRecommended { get; init; }
        public int HelpfulCount { get; init; }
        public int FunnyCount { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}
