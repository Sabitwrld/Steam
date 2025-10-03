using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.RegionalPrice;

namespace Steam.Application.Services.Store.Interfaces
{
    public interface IRegionalPriceService
    {
        Task<RegionalPriceReturnDto> CreateRegionalPriceAsync(RegionalPriceCreateDto dto);
        Task<RegionalPriceReturnDto> UpdateRegionalPriceAsync(int id, RegionalPriceUpdateDto dto);
        Task<bool> DeleteRegionalPriceAsync(int id);
        Task<RegionalPriceReturnDto> GetRegionalPriceByIdAsync(int id);
        Task<PagedResponse<RegionalPriceListItemDto>> GetAllRegionalPricesAsync(int pageNumber, int pageSize);
    }
}
