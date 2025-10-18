using Microsoft.AspNetCore.Identity;
using Steam.Application.DTOs.Admin;
using Steam.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Admin.Interfaces
{
    public interface IAdminService
    {
        Task<List<UserDto>> GetUsersWithRolesAsync();
        Task<List<IdentityRole>> GetRolesAsync();
        Task AssignRolesAsync(string userId, AssignRolesRequestDto request);
        Task<DashboardStatsDto> GetDashboardStatisticsAsync();
    }
}
