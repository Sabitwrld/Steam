using Steam.Application.DTOs.Catalog.SystemRequirements;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Catalog.Interfaces
{
    public interface ISystemRequirementsService
    {
        Task<SystemRequirementsReturnDto> GetByIdAsync(int id);
        Task<PagedResponse<SystemRequirementsListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<SystemRequirementsReturnDto> CreateAsync(SystemRequirementsCreateDto dto);
        Task<SystemRequirementsReturnDto> UpdateAsync(int id, SystemRequirementsUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
