using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Catalog.Interfaces
{
    public interface IGenreService
    {
        Task<GenreReturnDto> GetByIdAsync(int id);
        Task<PagedResponse<GenreListItemDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<GenreReturnDto> CreateAsync(GenreCreateDto dto);
        Task<GenreReturnDto> UpdateAsync(int id, GenreUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
