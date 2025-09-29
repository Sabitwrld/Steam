using Steam.Application.DTOs.Achievements.Badge;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Achievements.Interfaces
{
    public interface IBadgeService
    {
        Task<BadgeReturnDto> CreateBadgeAsync(BadgeCreateDto dto);
        Task<BadgeReturnDto> UpdateBadgeAsync(int id, BadgeUpdateDto dto);
        Task<bool> DeleteBadgeAsync(int id);
        Task<BadgeReturnDto> GetBadgeByIdAsync(int id);
        Task<PagedResponse<BadgeListItemDto>> GetAllBadgesAsync(int pageNumber, int pageSize);
    }
}
