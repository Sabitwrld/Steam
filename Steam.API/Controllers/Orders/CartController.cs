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
        public async Task<ActionResult<CartReturnDto>> GetCart(int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        [HttpPost("user/{userId}/items")]
        [ProducesResponseType(typeof(CartReturnDto), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CartReturnDto>> AddItem(int userId, [FromBody] CartItemCreateDto dto)
        {
            var cart = await _cartService.AddItemToCartAsync(userId, dto);
            return Ok(cart);
        }

        [HttpPut("user/{userId}/items/{cartItemId}")]
        [ProducesResponseType(typeof(CartReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CartReturnDto>> UpdateItemQuantity(int userId, int cartItemId, [FromBody] CartItemUpdateDto dto)
        {
            var cart = await _cartService.UpdateItemQuantityAsync(userId, cartItemId, dto.Quantity);
            return Ok(cart);
        }

        [HttpDelete("user/{userId}/items/{cartItemId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveItem(int userId, int cartItemId)
        {
            await _cartService.RemoveItemFromCartAsync(userId, cartItemId);
            return NoContent();
        }

        [HttpDelete("user/{userId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }
    }
}
