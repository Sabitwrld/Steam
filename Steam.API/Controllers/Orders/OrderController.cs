using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Orders.Order;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Orders.Interfaces;
using System.Security.Claims;

namespace Steam.API.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // All actions require login
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);

        /// <summary>
        /// Creates an order for the logged-in user from their cart.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(OrderReturnDto), 201)]
        public async Task<ActionResult<OrderReturnDto>> CreateOrderFromMyCart()
        {
            var userId = GetCurrentUserId();
            var order = await _orderService.CreateOrderFromCartAsync(userId);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        /// <summary>
        /// Gets a specific order by its ID. (Further checks inside service should verify ownership).
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderReturnDto>> GetOrderById(int id)
        {
            // Optional: Add a check here or in the service to ensure the user owns this order, or is an admin.
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        /// <summary>
        /// Gets the order history for the logged-in user.
        /// </summary>
        [HttpGet("my-orders")]
        [ProducesResponseType(typeof(PagedResponse<OrderListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<OrderListItemDto>>> GetMyOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userId = GetCurrentUserId();
            var orders = await _orderService.GetOrdersByUserIdAsync(userId, pageNumber, pageSize);
            return Ok(orders);
        }

        /// <summary>
        /// Updates the status of an order. (Admin only)
        /// </summary>
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(OrderReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderReturnDto>> UpdateStatus(int id, [FromQuery] string status)
        {
            var order = await _orderService.UpdateOrderStatusAsync(id, status);
            return Ok(order);
        }
    }
}
