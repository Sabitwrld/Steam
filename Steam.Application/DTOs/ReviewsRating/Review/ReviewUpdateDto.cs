namespace Steam.Application.DTOs.ReviewsRating.Review
{
    public record ReviewUpdateDto
    {
        public string Title { get; init; } = string.Empty;
        public string Content { get; init; } = string.Empty;
        public bool IsRecommended { get; init; }
        public bool? IsApproved { get; init; } // Əlavə edildi
    }
}
