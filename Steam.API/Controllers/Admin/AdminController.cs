using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Auth;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Auth.Interfaces;

namespace Steam.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // This entire controller is only for Admins
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        [ProducesResponseType(typeof(PagedResponse<UserDto>), 200)]
        public async Task<ActionResult<PagedResponse<UserDto>>> GetAllUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);
            return Ok(users);
        }

        [HttpGet("users/{userId}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> GetUserById(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            return Ok(user);
        }

        [HttpPost("roles/assign")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
        {
            await _userService.AssignRoleAsync(dto);
            return Ok(new { message = $"Role '{dto.RoleName}' was successfully assigned to the user." });
        }

        [HttpPost("roles/remove")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RemoveRole([FromBody] AssignRoleDto dto)
        {
            await _userService.RemoveRoleAsync(dto);
            return Ok(new { message = $"Role '{dto.RoleName}' was successfully removed from the user." });
        }
    }
}
