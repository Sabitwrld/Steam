using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.RegionalPrice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
