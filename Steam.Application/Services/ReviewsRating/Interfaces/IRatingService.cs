using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.ReviewsRating.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
