using Steam.Application.DTOs.Library.UserLibrary;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Library.Interfaces
{
    public interface IUserLibraryService
    {
        // --- MISSING METHODS ADDED HERE ---
        Task<UserLibraryReturnDto> CreateUserLibraryAsync(UserLibraryCreateDto dto);
        Task<UserLibraryReturnDto> UpdateUserLibraryAsync(int id, UserLibraryUpdateDto dto);
        Task<bool> DeleteUserLibraryAsync(int id);

        // --- Existing Methods ---
        Task<UserLibraryReturnDto> GetUserLibraryByUserIdAsync(int userId);
        Task<UserLibraryReturnDto> GetUserLibraryByIdAsync(int id);
        Task<PagedResponse<UserLibraryListItemDto>> GetAllUserLibrariesAsync(int pageNumber, int pageSize);
    }
}
