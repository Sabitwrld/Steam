using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.PricePoint;

namespace Steam.Application.Services.Store.Interfaces
{
    public interface IPricePointService
    {
        Task<PricePointReturnDto> CreatePricePointAsync(PricePointCreateDto dto);
        Task<PricePointReturnDto> UpdatePricePointAsync(int id, PricePointUpdateDto dto);
        Task<bool> DeletePricePointAsync(int id);
        Task<PricePointReturnDto> GetPricePointByIdAsync(int id);
        Task<PagedResponse<PricePointListItemDto>> GetAllPricePointsAsync(int pageNumber, int pageSize);
    }
}
