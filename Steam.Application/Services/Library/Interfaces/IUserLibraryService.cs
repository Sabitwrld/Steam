using Steam.Application.DTOs.Library.UserLibrary;
using Steam.Application.DTOs.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Library.Interfaces
{
    public interface IUserLibraryService
    {
        Task<UserLibraryReturnDto> CreateUserLibraryAsync(UserLibraryCreateDto dto);
        Task<UserLibraryReturnDto> UpdateUserLibraryAsync(int id, UserLibraryUpdateDto dto);
        Task<bool> DeleteUserLibraryAsync(int id);
        Task<UserLibraryReturnDto> GetUserLibraryByIdAsync(int id);
        Task<PagedResponse<UserLibraryListItemDto>> GetAllUserLibrariesAsync(int pageNumber, int pageSize);
    }
}
