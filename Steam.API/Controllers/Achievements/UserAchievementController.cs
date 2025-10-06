using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Achievements.UserAchievement;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Achievements.Interfaces;

namespace Steam.API.Controllers.Achievements
{
    [Route("api/user-achievements")]
    [ApiController]
    public class UserAchievementController : ControllerBase
    {
        private readonly IUserAchievementService _service;

        public UserAchievementController(IUserAchievementService service)
        {
            _service = service;
        }

        // Unlocks an achievement for a user
        [HttpPost("unlock")]
        [ProducesResponseType(typeof(UserAchievementReturnDto), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserAchievementReturnDto>> UnlockAchievement([FromBody] UserAchievementCreateDto dto)
        {
            var result = await _service.UnlockAchievementAsync(dto);
            return Ok(result);
        }

        // Gets all achievements for a specific user
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(PagedResponse<UserAchievementListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<UserAchievementListItemDto>>> GetAchievementsForUser(string userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAchievementsForUserAsync(userId, pageNumber, pageSize);
            return Ok(result);
        }

        // Gets a specific unlocked achievement record by its own ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserAchievementReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserAchievementReturnDto>> GetById(int id)
        {
            var result = await _service.GetUserAchievementByIdAsync(id);
            return Ok(result);
        }

        // Deletes an unlocked achievement record (for admin/testing purposes)
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteUserAchievementAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
