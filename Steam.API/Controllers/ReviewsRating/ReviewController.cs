using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.ReviewsRating.Review;
using Steam.Application.Services.ReviewsRating.Interfaces;

namespace Steam.API.Controllers.ReviewsRating
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReviewReturnDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ReviewReturnDto>> CreateReview([FromBody] ReviewCreateDto dto)
        {
            var result = await _service.CreateReviewAsync(dto);
            return CreatedAtAction(nameof(GetReviewById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ReviewReturnDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ReviewReturnDto>> UpdateReview(int id, [FromBody] ReviewUpdateDto dto)
        {
            // Note: In a real-world scenario, you would get the userId from the JWT token.
            // For now, we'll assume it's passed or handled elsewhere.
            var userId = "user-id-from-token"; // Placeholder
            var result = await _service.UpdateReviewAsync(id, userId, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteReview(int id)
        {
            // Note: In a real-world scenario, you would get the userId from the JWT token.
            var userId = "user-id-from-token"; // Placeholder
            var success = await _service.DeleteReviewAsync(id, userId);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReviewReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ReviewReturnDto>> GetReviewById(int id)
        {
            var result = await _service.GetReviewByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("application/{applicationId}")]
        [ProducesResponseType(typeof(PagedResponse<ReviewListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<ReviewListItemDto>>> GetReviewsForApplication(int applicationId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetReviewsForApplicationAsync(applicationId, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost("{id}/helpful")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> MarkAsHelpful(int id)
        {
            await _service.MarkAsHelpfulAsync(id);
            return Ok(new { message = "Review marked as helpful." });
        }

        [HttpPost("{id}/funny")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> MarkAsFunny(int id)
        {
            await _service.MarkAsFunnyAsync(id);
            return Ok(new { message = "Review marked as funny." });
        }
    }
}
