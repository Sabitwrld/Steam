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
        [ProducesResponseType(typeof(UserLibraryReturnDto), 200)]
        [ProducesResponseType(404)]
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

        // This endpoint is more for admin purposes to see all libraries
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<UserLibraryListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<UserLibraryListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _userLibraryService.GetAllUserLibrariesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserLibraryReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserLibraryReturnDto>> GetById(int id)
        {
            var result = await _userLibraryService.GetUserLibraryByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserLibraryReturnDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserLibraryReturnDto>> Create([FromBody] UserLibraryCreateDto dto)
        {
            var result = await _userLibraryService.CreateUserLibraryAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserLibraryReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserLibraryReturnDto>> Update(int id, [FromBody] UserLibraryUpdateDto dto)
        {
            var result = await _userLibraryService.UpdateUserLibraryAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userLibraryService.DeleteUserLibraryAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
