using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.ReviewsRating.Rating;

namespace Steam.Application.Services.ReviewsRating.Interfaces
{
    public interface IRatingService
    {
        Task<RatingReturnDto> CreateRatingAsync(RatingCreateDto dto);
        Task<RatingReturnDto> UpdateRatingAsync(int id, RatingUpdateDto dto);
        Task<bool> DeleteRatingAsync(int id);
        Task<RatingReturnDto> GetRatingByIdAsync(int id);
        Task<PagedResponse<RatingListItemDto>> GetAllRatingsAsync(int pageNumber, int pageSize);
    }
}
