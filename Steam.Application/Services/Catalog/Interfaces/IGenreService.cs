using Steam.Application.DTOs.Catalog.Application;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Catalog.Interfaces
{
    public interface IGenreService
    {
        Task<GenreReturnDto> CreateGenreAsync(GenreCreateDto dto);
        Task<GenreReturnDto> UpdateGenreAsync(int id, GenreUpdateDto dto);
        Task<bool> DeleteGenreAsync(int id);
        Task<GenreReturnDto> GetGenreByIdAsync(int id);
        Task<PagedResponse<GenreListItemDto>> GetAllGenreAsync(int pageNumber, int pageSize);
    }
}
