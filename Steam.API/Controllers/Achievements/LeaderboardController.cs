using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Achievements.Leaderboard;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Achievements.Interfaces;

namespace Steam.API.Controllers.Achievements
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _service;

        public LeaderboardController(ILeaderboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<LeaderboardListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllLeaderboardsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaderboardReturnDto>> GetById(int id)
        {
            var result = await _service.GetLeaderboardByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<LeaderboardReturnDto>> Create([FromBody] LeaderboardCreateDto dto)
        {
            var result = await _service.CreateLeaderboardAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LeaderboardReturnDto>> Update(int id, [FromBody] LeaderboardUpdateDto dto)
        {
            var result = await _service.UpdateLeaderboardAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteLeaderboardAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
