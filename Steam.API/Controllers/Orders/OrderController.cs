using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Orders.Order;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Orders.Interfaces;

namespace Steam.API.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderReturnDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<OrderReturnDto>> CreateOrder([FromBody] OrderCreateDto dto)
        {
            var order = await _orderService.CreateOrderFromCartAsync(dto.UserId);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderReturnDto>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(PagedResponse<OrderListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<OrderListItemDto>>> GetOrdersForUser(int userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var orders = await _orderService.GetOrdersForUserAsync(userId, pageNumber, pageSize);
            return Ok(orders);
        }

        // This endpoint would be for admin use
        [HttpPut("{id}/status")]
        [ProducesResponseType(typeof(OrderReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderReturnDto>> UpdateStatus(int id, [FromQuery] string status)
        {
            var order = await _orderService.UpdateOrderStatusAsync(id, status);
            return Ok(order);
        }
    }
}
