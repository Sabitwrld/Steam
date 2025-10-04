using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.ReviewsRating.Review;

namespace Steam.Application.Services.ReviewsRating.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewReturnDto> CreateReviewAsync(ReviewCreateDto dto);
        Task<ReviewReturnDto> UpdateReviewAsync(int reviewId, string userId, ReviewUpdateDto dto);
        Task<bool> DeleteReviewAsync(int reviewId, string userId);
        Task<ReviewReturnDto> GetReviewByIdAsync(int id);
        Task<PagedResponse<ReviewListItemDto>> GetReviewsForApplicationAsync(int applicationId, int pageNumber, int pageSize);

        // New methods for community feedback
        Task MarkAsHelpfulAsync(int reviewId);
        Task MarkAsFunnyAsync(int reviewId);
    }
}
