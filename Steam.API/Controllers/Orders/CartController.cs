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
    [Authorize] // This entire controller now requires a user to be logged in.
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Helper method to get the current user's ID from the token claims.
        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        /// <summary>
        /// Gets the cart for the currently logged-in user.
        /// </summary>
        [HttpGet] // Route is now just /api/cart
        [ProducesResponseType(typeof(CartReturnDto), 200)]
        public async Task<ActionResult<CartReturnDto>> GetMyCart()
        {
            var userId = GetCurrentUserId();
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        /// <summary>
        /// Adds an item to the logged-in user's cart.
        /// </summary>
        [HttpPost("items")]
        [ProducesResponseType(typeof(CartReturnDto), 200)]
        public async Task<ActionResult<CartReturnDto>> AddItemToMyCart([FromBody] CartItemCreateDto dto)
        {
            var userId = GetCurrentUserId();
            var cart = await _cartService.AddItemToCartAsync(userId, dto);
            return Ok(cart);
        }

        /// <summary>
        /// Updates the quantity of an item in the logged-in user's cart.
        /// </summary>
        [HttpPut("items/{cartItemId}")]
        [ProducesResponseType(typeof(CartReturnDto), 200)]
        public async Task<ActionResult<CartReturnDto>> UpdateItemQuantityInMyCart(int cartItemId, [FromBody] CartItemUpdateDto dto)
        {
            var userId = GetCurrentUserId();
            var cart = await _cartService.UpdateItemQuantityAsync(userId, cartItemId, dto.Quantity);
            return Ok(cart);
        }

        /// <summary>
        /// Removes an item from the logged-in user's cart.
        /// </summary>
        [HttpDelete("items/{cartItemId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> RemoveItemFromMyCart(int cartItemId)
        {
            var userId = GetCurrentUserId();
            await _cartService.RemoveItemFromCartAsync(userId, cartItemId);
            return NoContent();
        }

        /// <summary>
        /// Clears all items from the logged-in user's cart.
        /// </summary>
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
