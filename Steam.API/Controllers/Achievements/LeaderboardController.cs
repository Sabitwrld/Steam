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

        // Gets the leaderboard for a specific application
        [HttpGet("application/{applicationId}")]
        [ProducesResponseType(typeof(PagedResponse<LeaderboardListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<LeaderboardListItemDto>>> GetLeaderboard(int applicationId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 25)
        {
            var result = await _service.GetLeaderboardForApplicationAsync(applicationId, pageNumber, pageSize);
            return Ok(result);
        }

        // Adds or updates a user's score on a leaderboard
        [HttpPost("score")]
        [ProducesResponseType(typeof(LeaderboardReturnDto), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<LeaderboardReturnDto>> AddOrUpdateScore([FromBody] LeaderboardCreateDto dto)
        {
            var result = await _service.AddOrUpdateScoreAsync(dto);
            return Ok(result);
        }

        // Other standard CRUD endpoints for admin use
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaderboardReturnDto>> GetById(int id)
        {
            var result = await _service.GetScoreByIdAsync(id);
            return Ok(result);
        }
    }
}
