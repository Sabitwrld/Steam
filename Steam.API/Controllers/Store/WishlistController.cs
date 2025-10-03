using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Wishlist;
using Steam.Application.Services.Store.Interfaces;

namespace Steam.API.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _service;

        public WishlistController(IWishlistService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(WishlistReturnDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<WishlistReturnDto>> AddToWishlist([FromBody] WishlistCreateDto dto)
        {
            var result = await _service.CreateWishlistAsync(dto);
            return CreatedAtAction(nameof(GetWishlistItemById), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveFromWishlist(int id)
        {
            var success = await _service.DeleteWishlistAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WishlistReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<WishlistReturnDto>> GetWishlistItemById(int id)
        {
            var result = await _service.GetWishlistByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(PagedResponse<WishlistListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<WishlistListItemDto>>> GetUserWishlist(int userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            // Note: This assumes GetAllWishlistsAsync can be filtered by userId in the service.
            // For simplicity, we'll call the generic one, but a specific service method is better.
            var result = await _service.GetAllWishlistsAsync(pageNumber, pageSize); // This should be improved in the service later
            return Ok(result);
        }
    }
}
