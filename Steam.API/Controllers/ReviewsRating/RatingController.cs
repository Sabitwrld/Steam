using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.ReviewsRating.Rating;
using Steam.Application.Services.ReviewsRating.Interfaces;

namespace Steam.API.Controllers.ReviewsRating
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<RatingListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _ratingService.GetAllRatingsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RatingReturnDto>> GetById(int id)
        {
            var result = await _ratingService.GetRatingByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RatingReturnDto>> Create([FromBody] RatingCreateDto dto)
        {
            var result = await _ratingService.CreateRatingAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RatingReturnDto>> Update(int id, [FromBody] RatingUpdateDto dto)
        {
            var result = await _ratingService.UpdateRatingAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _ratingService.DeleteRatingAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

