using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.RegionalPrice;
using Steam.Application.Services.Store.Interfaces;

namespace Steam.API.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionalPriceController : ControllerBase
    {
        private readonly IRegionalPriceService _service;

        public RegionalPriceController(IRegionalPriceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<RegionalPriceListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllRegionalPricesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegionalPriceReturnDto>> GetById(int id)
        {
            var result = await _service.GetRegionalPriceByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<RegionalPriceReturnDto>> Create([FromBody] RegionalPriceCreateDto dto)
        {
            var result = await _service.CreateRegionalPriceAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RegionalPriceReturnDto>> Update(int id, [FromBody] RegionalPriceUpdateDto dto)
        {
            var result = await _service.UpdateRegionalPriceAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteRegionalPriceAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
