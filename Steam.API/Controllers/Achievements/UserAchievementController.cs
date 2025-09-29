using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Achievements.UserAchievement;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Achievements.Interfaces;

namespace Steam.API.Controllers.Achievements
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAchievementController : ControllerBase
    {
        private readonly IUserAchievementService _service;

        public UserAchievementController(IUserAchievementService service)
        {
            _service = service;
        }

        // GET: api/UserAchievement
        [HttpGet]
        public async Task<ActionResult<PagedResponse<UserAchievementListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllUserAchievementsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/UserAchievement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAchievementReturnDto>> GetById(int id)
        {
            var result = await _service.GetUserAchievementByIdAsync(id);
            return Ok(result);
        }

        // POST: api/UserAchievement
        [HttpPost]
        public async Task<ActionResult<UserAchievementReturnDto>> Create([FromBody] UserAchievementCreateDto dto)
        {
            var result = await _service.CreateUserAchievementAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/UserAchievement/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserAchievementReturnDto>> Update(int id, [FromBody] UserAchievementUpdateDto dto)
        {
            var result = await _service.UpdateUserAchievementAsync(id, dto);
            return Ok(result);
        }

        // DELETE: api/UserAchievement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteUserAchievementAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
