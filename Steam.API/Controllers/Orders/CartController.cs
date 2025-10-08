using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Orders.Cart;
using Steam.Application.DTOs.Orders.CartItem;
using Steam.Application.Services.Orders.Interfaces;
using System.Security.Claims;

namespace Steam.API.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // This entire controller now requires a user to be logged in
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Helper to get the current user's ID from the token claims
        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        [HttpGet] // Route is now just /api/cart, gets the cart of the logged-in user
        [ProducesResponseType(typeof(CartReturnDto), 200)]
        public async Task<ActionResult<CartReturnDto>> GetMyCart()
        {
            var userId = GetCurrentUserId();
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        [HttpPost("items")]
        [ProducesResponseType(typeof(CartReturnDto), 200)]
        public async Task<ActionResult<CartReturnDto>> AddItemToMyCart([FromBody] CartItemCreateDto dto)
        {
            var userId = GetCurrentUserId();
            var cart = await _cartService.AddItemToCartAsync(userId, dto);
            return Ok(cart);
        }

        [HttpPut("items/{cartItemId}")]
        [ProducesResponseType(typeof(CartReturnDto), 200)]
        public async Task<ActionResult<CartReturnDto>> UpdateItemQuantityInMyCart(int cartItemId, [FromBody] CartItemUpdateDto dto)
        {
            var userId = GetCurrentUserId();
            var cart = await _cartService.UpdateItemQuantityAsync(userId, cartItemId, dto.Quantity);
            return Ok(cart);
        }

        [HttpDelete("items/{cartItemId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> RemoveItemFromMyCart(int cartItemId)
        {
            var userId = GetCurrentUserId();
            await _cartService.RemoveItemFromCartAsync(userId, cartItemId);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        public async Task<IActionResult> ClearMyCart()
        {
            var userId = GetCurrentUserId();
            await _cartService.ClearCartAsync(userId);
            return NoContent();
        }
    }
}
