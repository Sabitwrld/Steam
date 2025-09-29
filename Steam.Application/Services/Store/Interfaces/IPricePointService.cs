using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.PricePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
