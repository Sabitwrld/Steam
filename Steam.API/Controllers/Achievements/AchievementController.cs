using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Achievements.Achievements;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Achievements.Interfaces;

namespace Steam.API.Controllers.Achievements
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly IAchievementService _service;

        public AchievementController(IAchievementService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<AchievementListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<AchievementListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllAchievementsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AchievementReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AchievementReturnDto>> GetById(int id)
        {
            var result = await _service.GetAchievementByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(AchievementReturnDto), 201)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AchievementReturnDto>> Create([FromBody] AchievementCreateDto dto)
        {
            var result = await _service.CreateAchievementAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AchievementReturnDto), 200)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AchievementReturnDto>> Update(int id, [FromBody] AchievementUpdateDto dto)
        {
            var result = await _service.UpdateAchievementAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAchievementAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
