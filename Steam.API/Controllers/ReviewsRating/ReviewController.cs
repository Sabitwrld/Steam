using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.ReviewsRating.Review;
using Steam.Application.Services.ReviewsRating.Interfaces;
using System.Security.Claims;

namespace Steam.API.Controllers.ReviewsRating
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;
        public ReviewController(IReviewService service) { _service = service; }

        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpGet("application/{applicationId}")]
        public async Task<ActionResult<PagedResponse<ReviewListItemDto>>> GetReviewsForApplication(int applicationId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetReviewsForApplicationAsync(applicationId, pageNumber, pageSize);
            return Ok(result);
        }

        // ... digər endpointlər ...

        /// <summary>
        /// Gets all reviews for the admin panel with pagination. (Admin only)
        /// </summary>
        [HttpGet("paged")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(PagedResponse<ReviewListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<ReviewListItemDto>>> GetAllReviews([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] bool? isApproved = null)
        {
            var result = await _service.GetAllReviewsAsync(pageNumber, pageSize, isApproved);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewReturnDto>> GetReviewById(int id)
        {
            var result = await _service.GetReviewByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReviewReturnDto>> CreateMyReview([FromBody] ReviewCreateDtoForUser dto)
        {
            var userId = GetCurrentUserId();
            var createDto = new ReviewCreateDto
            {
                UserId = userId,
                ApplicationId = dto.ApplicationId,
                Title = dto.Title,
                Content = dto.Content,
                IsRecommended = dto.IsRecommended
            };
            var result = await _service.CreateReviewAsync(createDto);
            return CreatedAtAction(nameof(GetReviewById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")] // Dəyişdirildi
        public async Task<ActionResult<ReviewReturnDto>> UpdateMyReview(int id, [FromBody] ReviewUpdateDto dto)
        {
            var userId = GetCurrentUserId();
            // Admin rolundakı istifadəçi üçün userId boş ola bilər, bu halda servis yoxlamanı özü edəcək
            var result = await _service.UpdateReviewAsync(id, userId, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMyReview(int id)
        {
            var userId = GetCurrentUserId();
            var success = await _service.DeleteReviewAsync(id, userId);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPost("{id}/helpful")]
        [Authorize]
        public async Task<IActionResult> MarkAsHelpful(int id)
        {
            await _service.MarkAsHelpfulAsync(id);
            return Ok(new { message = "Review marked as helpful." });
        }

        [HttpPost("{id}/funny")]
        [Authorize]
        public async Task<IActionResult> MarkAsFunny(int id)
        {
            await _service.MarkAsFunnyAsync(id);
            return Ok(new { message = "Review marked as funny." });
        }
    }
}
