using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.ReviewsRating.Review;
using Steam.Application.Services.ReviewsRating.Interfaces;

namespace Steam.API.Controllers.ReviewsRating
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<ReviewListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _reviewService.GetAllReviewsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewReturnDto>> GetById(int id)
        {
            var result = await _reviewService.GetReviewByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ReviewReturnDto>> Create([FromBody] ReviewCreateDto dto)
        {
            var result = await _reviewService.CreateReviewAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewReturnDto>> Update(int id, [FromBody] ReviewUpdateDto dto)
        {
            var result = await _reviewService.UpdateReviewAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reviewService.DeleteReviewAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
