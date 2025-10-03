using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Orders.Refund;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Orders.Interfaces;

namespace Steam.API.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundController : ControllerBase
    {
        private readonly IRefundService _refundService;

        public RefundController(IRefundService refundService)
        {
            _refundService = refundService;
        }

        [HttpPost("request")]
        [ProducesResponseType(typeof(RefundReturnDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RefundReturnDto>> RequestRefund([FromBody] RefundCreateDto dto)
        {
            var result = await _refundService.RequestRefundAsync(dto);
            return CreatedAtAction(nameof(GetRefundById), new { id = result.Id }, result);
        }

        // This endpoint would be for admin use
        [HttpPut("{id}/status")]
        [ProducesResponseType(typeof(RefundReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RefundReturnDto>> UpdateRefundStatus(int id, [FromQuery] string status)
        {
            var result = await _refundService.UpdateRefundStatusAsync(id, status);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RefundReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<RefundReturnDto>> GetRefundById(int id)
        {
            var result = await _refundService.GetRefundByIdAsync(id);
            return Ok(result);
        }

        // This endpoint would be for admin use
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<RefundListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<RefundListItemDto>>> GetAllRefunds([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _refundService.GetAllRefundsAsync(pageNumber, pageSize);
            return Ok(result);
        }
    }
}
