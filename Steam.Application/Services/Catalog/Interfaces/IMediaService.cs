using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Catalog.Media;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Catalog.Interfaces
{
    public interface IMediaService 
    {
        Task<MediaReturnDto> CreateMediaAsync(MediaCreateDto dto);
        Task<MediaReturnDto> UpdateMediaAsync(int id, MediaUpdateDto dto);
        Task<bool> DeleteMediaAsync(int id);
        Task<MediaReturnDto> GetMediaByIdAsync(int id);
        Task<PagedResponse<MediaListItemDto>> GetAllMediaAsync(int pageNumber, int pageSize);
    }
}
