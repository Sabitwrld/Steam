using Steam.Application.DTOs.Auth;
using Steam.Application.DTOs.Pagination;

namespace Steam.Application.Services.Auth.Interfaces
{
    public interface IUserService
    {
        Task<PagedResponse<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task AssignRoleAsync(AssignRoleDto dto);
        Task RemoveRoleAsync(AssignRoleDto dto);
    }
}
