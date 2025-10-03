using Steam.Application.DTOs.Catalog.Application;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Catalog.Media;
using Steam.Application.DTOs.Catalog.SystemRequirements;
using Steam.Application.DTOs.Catalog.Tag;
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
        Task<PagedResponse<ApplicationCatalogListItemDto>> GetAllAsync(
               int pageNumber, int pageSize, string? searchTerm, int? genreId, int? tagId);
        Task<ApplicationCatalogReturnDto> CreateAsync(ApplicationCatalogCreateDto dto);
        Task<ApplicationCatalogReturnDto> UpdateAsync(int id, ApplicationCatalogUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<GenreListItemDto>> GetGenresByApplicationIdAsync(int applicationId);
        Task<List<TagListItemDto>> GetTagsByApplicationIdAsync(int applicationId); // YENİ
        Task<List<MediaListItemDto>> GetMediaByApplicationIdAsync(int applicationId); // YENİ
        Task<List<SystemRequirementsListItemDto>> GetSystemRequirementsByApplicationIdAsync(int applicationId); // YENİ

    }
}
