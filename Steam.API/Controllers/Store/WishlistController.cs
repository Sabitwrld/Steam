using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Wishlist;
using Steam.Application.Services.Store.Interfaces;
using System.Security.Claims;

namespace Steam.API.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All actions require login
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _service;

        public WishlistController(IWishlistService service)
        {
            _service = service;
        }

        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        /// <summary>
        /// Gets the wishlist for the currently logged-in user.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<WishlistListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<WishlistListItemDto>>> GetMyWishlist([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userId = GetCurrentUserId();
            var result = await _service.GetWishlistByUserIdAsync(userId, pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Adds an application to the logged-in user's wishlist.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(WishlistReturnDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<WishlistReturnDto>> AddToMyWishlist([FromBody] WishlistCreateDtoForUser dto)
        {
            var userId = GetCurrentUserId();
            var createDto = new WishlistCreateDto { UserId = userId, ApplicationId = dto.ApplicationId };
            var result = await _service.CreateWishlistAsync(createDto);
            return CreatedAtAction(nameof(GetWishlistItemById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Removes an item from the logged-in user's wishlist by Application ID.
        /// </summary>
        [HttpDelete("application/{applicationId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveFromMyWishlist(int applicationId)
        {
            var userId = GetCurrentUserId();
            var success = await _service.DeleteWishlistByAppIdAsync(userId, applicationId);
            if (!success)
            {
                return NotFound("Item not found in your wishlist.");
            }
            return NoContent();
        }

        // This endpoint is more for admin/dev use.
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WishlistReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<WishlistReturnDto>> GetWishlistItemById(int id)
        {
            var result = await _service.GetWishlistByIdAsync(id);
            return Ok(result);
        }
    }
}
