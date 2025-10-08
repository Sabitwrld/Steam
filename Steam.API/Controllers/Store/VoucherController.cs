using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Voucher;
using Steam.Application.Services.Store.Interfaces;

namespace Steam.API.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // BÜTÜN CONTROLLER-İ QORU
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _service;

        public VoucherController(IVoucherService service)
        {
            _service = service;
        }

        // Note: This endpoint would typically be restricted to administrators.
        [HttpPost]
        [ProducesResponseType(typeof(VoucherReturnDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<VoucherReturnDto>> CreateVoucher([FromBody] VoucherCreateDto dto)
        {
            var result = await _service.CreateVoucherAsync(dto);
            return CreatedAtAction(nameof(GetVoucherById), new { id = result.Id }, result);
        }

        [HttpPost("redeem")]
        [ProducesResponseType(typeof(VoucherReturnDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<VoucherReturnDto>> RedeemVoucher([FromQuery] string code, [FromQuery] int userId)
        {
            var result = await _service.RedeemVoucherAsync(code, userId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(VoucherReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<VoucherReturnDto>> GetVoucherById(int id)
        {
            var result = await _service.GetVoucherByIdAsync(id);
            return Ok(result);
        }

        // Note: This endpoint would typically be restricted to administrators.
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<VoucherListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<VoucherListItemDto>>> GetAllVouchers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllVouchersAsync(pageNumber, pageSize);
            return Ok(result);
        }
    }
}
