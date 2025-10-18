using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Steam.Application.DTOs.Admin;
using Steam.Application.DTOs.Auth;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Admin.Interfaces;
using Steam.Application.Services.Auth.Interfaces;
using Steam.Domain.Entities.Identity;
using Steam.Infrastructure.Repositories.Interfaces;

namespace Steam.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // This entire controller is only for Admins
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _adminService.GetUsersWithRolesAsync();
            return Ok(users);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _adminService.GetRolesAsync();
            return Ok(roles);
        }

        [HttpPost("assign-role/{userId}")]
        public async Task<IActionResult> AssignRoles(string userId, [FromBody] AssignRolesRequestDto request)
        {
            await _adminService.AssignRolesAsync(userId, request);
            return Ok(new { message = "User roles updated successfully." });
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var stats = await _adminService.GetDashboardStatisticsAsync();
            return Ok(stats);
        }
    }


}
