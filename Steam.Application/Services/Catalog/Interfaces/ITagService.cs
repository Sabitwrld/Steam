using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Catalog.Tag;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Catalog.Interfaces
{
    public interface ITagService
    {
        Task<TagReturnDto> GetByIdAsync(int id);
        Task<PagedResponse<TagListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<TagReturnDto> CreateAsync(TagCreateDto dto);
        Task<TagReturnDto> UpdateAsync(int id, TagUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
