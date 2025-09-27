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
        Task<ApplicationCatalogReturnDto> CreateApplicationCatalogAsync(ApplicationCatalogCreateDto dto);
        Task<ApplicationCatalogReturnDto> UpdateApplicationCatalogAsync(int id, ApplicationCatalogUpdateDto dto);
        Task<bool> DeleteApplicationCatalogAsync(int id);
        Task<ApplicationCatalogReturnDto> GetApplicationCatalogByIdAsync(int id);
        Task<PagedResponse<ApplicationCatalogListItemDto>> GetAllApplicationCatalogAsync(int pageNumber, int pageSize);
    }
}
