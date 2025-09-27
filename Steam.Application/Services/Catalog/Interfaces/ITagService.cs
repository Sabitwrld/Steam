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
        Task<TagReturnDto> CreateTagAsync(TagCreateDto dto);
        Task<TagReturnDto> UpdateTagAsync(int id, TagUpdateDto dto);
        Task<bool> DeleteTagAsync(int id);
        Task<TagReturnDto> GetTagByIdAsync(int id);
        Task<PagedResponse<TagListItemDto>> GetAllTagAsync(int pageNumber, int pageSize);
    }
}
