using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Library.UserLibrary;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Library.Interfaces;
using System.Security.Claims;

namespace Steam.API.Controllers.Library
{
    [Route("api/user-library")]
    [ApiController]
    public class UserLibraryController : ControllerBase
    {
        private readonly IUserLibraryService _userLibraryService;

        public UserLibraryController(IUserLibraryService userLibraryService)
        {
            _userLibraryService = userLibraryService;
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin")] // Yalnız Admin baxa bilər
        public async Task<ActionResult<UserLibraryReturnDto>> GetByUserId(string userId)
        {
            var result = await _userLibraryService.GetUserLibraryByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpGet("my-library")] // Endpoint-in adını daha anlaşıqlı edin
        [Authorize] // Yalnız login olmuş istifadəçilər
        public async Task<ActionResult<UserLibraryReturnDto>> GetMyLibrary()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var result = await _userLibraryService.GetUserLibraryByUserIdAsync(userId);
            return Ok(result);
        }

        // Bu endpoint admin paneli üçün faydalıdır
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PagedResponse<UserLibraryListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _userLibraryService.GetAllUserLibrariesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")] // Bu da adminə aid olmalıdır
        public async Task<ActionResult<UserLibraryReturnDto>> GetById(int id)
        {
            var result = await _userLibraryService.GetUserLibraryByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserLibraryReturnDto>> Create([FromBody] UserLibraryCreateDto dto)
        {
            var result = await _userLibraryService.CreateUserLibraryAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserLibraryReturnDto>> Update(int id, [FromBody] UserLibraryUpdateDto dto)
        {
            var result = await _userLibraryService.UpdateUserLibraryAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userLibraryService.DeleteUserLibraryAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
