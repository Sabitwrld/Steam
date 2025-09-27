using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Catalog.SystemRequirements;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Catalog.Interfaces
{
    public interface ISystemRequirementsService
    {
        Task<SystemRequirementsReturnDto> CreateSystemRequirementsAsync(SystemRequirementsCreateDto dto);
        Task<SystemRequirementsReturnDto> UpdateSystemRequirementsAsync(int id, SystemRequirementsUpdateDto dto);
        Task<bool> DeleteSystemRequirementsAsync(int id);
        Task<SystemRequirementsReturnDto> GetSystemRequirementsByIdAsync(int id);
        Task<PagedResponse<SystemRequirementsListItemDto>> GetAllSystemRequirementsAsync(int pageNumber, int pageSize);
    }
}
