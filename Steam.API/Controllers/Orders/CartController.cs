using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Orders.Cart;
using Steam.Application.DTOs.Orders.CartItem;
using Steam.Application.Services.Orders.Interfaces;

namespace Steam.API.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(CartReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CartReturnDto>> GetCart(string userId) // FIXED
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        [HttpPost("user/{userId}/items")]
        [ProducesResponseType(typeof(CartReturnDto), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CartReturnDto>> AddItem(string userId, [FromBody] CartItemCreateDto dto) // FIXED
        {
            var cart = await _cartService.AddItemToCartAsync(userId, dto);
            return Ok(cart);
        }

        [HttpPut("user/{userId}/items/{cartItemId}")]
        [ProducesResponseType(typeof(CartReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CartReturnDto>> UpdateItemQuantity(string userId, int cartItemId, [FromBody] CartItemUpdateDto dto) // FIXED
        {
            var cart = await _cartService.UpdateItemQuantityAsync(userId, cartItemId, dto.Quantity);
            return Ok(cart);
        }

        [HttpDelete("user/{userId}/items/{cartItemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveItem(string userId, int cartItemId) // FIXED
        {
            await _cartService.RemoveItemFromCartAsync(userId, cartItemId);
            return NoContent();
        }

        [HttpDelete("user/{userId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> ClearCart(string userId) // FIXED
        {
            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }
    }
}
