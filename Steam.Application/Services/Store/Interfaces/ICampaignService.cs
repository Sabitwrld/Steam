using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Campaign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Store.Interfaces
{
    public interface ICampaignService
    {
        Task<CampaignReturnDto> CreateCampaignAsync(CampaignCreateDto dto);
        Task<CampaignReturnDto> UpdateCampaignAsync(int id, CampaignUpdateDto dto);
        Task<bool> DeleteCampaignAsync(int id);
        Task<CampaignReturnDto> GetCampaignByIdAsync(int id);
        Task<PagedResponse<CampaignListItemDto>> GetAllCampaignsAsync(int pageNumber, int pageSize);
    }
}
