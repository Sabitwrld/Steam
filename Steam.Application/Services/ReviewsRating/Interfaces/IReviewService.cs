using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.ReviewsRating.Review;

namespace Steam.Application.Services.ReviewsRating.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewReturnDto> CreateReviewAsync(ReviewCreateDto dto);
        Task<ReviewReturnDto> UpdateReviewAsync(int id, ReviewUpdateDto dto);
        Task<bool> DeleteReviewAsync(int id);
        Task<ReviewReturnDto> GetReviewByIdAsync(int id);
        Task<PagedResponse<ReviewListItemDto>> GetAllReviewsAsync(int pageNumber, int pageSize);
    }
}
