using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Coupon;
using Steam.Application.Services.Store.Interfaces;

namespace Steam.API.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // BÜTÜN CONTROLLER-İ QORU
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _service;

        public CouponController(ICouponService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<CouponListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllCouponsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CouponReturnDto>> GetById(int id)
        {
            var result = await _service.GetCouponByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CouponReturnDto>> Create([FromBody] CouponCreateDto dto)
        {
            var result = await _service.CreateCouponAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CouponReturnDto>> Update(int id, [FromBody] CouponUpdateDto dto)
        {
            var result = await _service.UpdateCouponAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteCouponAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
