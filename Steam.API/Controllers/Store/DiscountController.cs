using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Discount;
using Steam.Application.Services.Store.Interfaces;

namespace Steam.API.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _service;

        public DiscountController(IDiscountService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<DiscountListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllDiscountsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountReturnDto>> GetById(int id)
        {
            var result = await _service.GetDiscountByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<DiscountReturnDto>> Create([FromBody] DiscountCreateDto dto)
        {
            var result = await _service.CreateDiscountAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DiscountReturnDto>> Update(int id, [FromBody] DiscountUpdateDto dto)
        {
            var result = await _service.UpdateDiscountAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteDiscountAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
