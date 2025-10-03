using Steam.Application.DTOs.Catalog.Media;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Catalog.Interfaces
{
    public interface IMediaService
    {
        Task<MediaReturnDto> GetByIdAsync(int id);
        Task<PagedResponse<MediaListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<MediaReturnDto> CreateAsync(MediaCreateDto dto);
        Task<MediaReturnDto> CreateWithFileAsync(MediaUploadDto dto); // Add this new method
        Task<MediaReturnDto> UpdateAsync(int id, MediaUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
