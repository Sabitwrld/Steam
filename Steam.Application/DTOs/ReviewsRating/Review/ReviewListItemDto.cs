namespace Steam.Application.DTOs.ReviewsRating.Review
{
    public record ReviewListItemDto
    {
        public int Id { get; init; }
        public string UserId { get; init; } = default!;
        public string UserName { get; init; } = default!; // Added for display
        public string Title { get; init; } = string.Empty;
        public string ContentShort { get; init; } = string.Empty; // A shortened version of the content
        public bool IsRecommended { get; init; }
        public int HelpfulCount { get; init; }
        public int FunnyCount { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
