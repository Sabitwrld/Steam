using Steam.Application.DTOs.Catalog.Application;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Catalog.Interfaces
{
    public interface IApplicationCatalogService
    {
        Task<ApplicationCatalogReturnDto> GetByIdAsync(int id);
        Task<PagedResponse<ApplicationCatalogListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<ApplicationCatalogReturnDto> CreateAsync(ApplicationCatalogCreateDto dto);
        Task<ApplicationCatalogReturnDto> UpdateAsync(int id, ApplicationCatalogUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
