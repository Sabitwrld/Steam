using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Achievements.Badge;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Achievements.Interfaces;

namespace Steam.API.Controllers.Achievements
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        private readonly IBadgeService _service;

        public BadgeController(IBadgeService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<BadgeListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<BadgeListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllBadgesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BadgeReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BadgeReturnDto>> GetById(int id)
        {
            var result = await _service.GetBadgeByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BadgeReturnDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BadgeReturnDto>> Create([FromBody] BadgeCreateDto dto)
        {
            var result = await _service.CreateBadgeAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BadgeReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BadgeReturnDto>> Update(int id, [FromBody] BadgeUpdateDto dto)
        {
            var result = await _service.UpdateBadgeAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteBadgeAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
