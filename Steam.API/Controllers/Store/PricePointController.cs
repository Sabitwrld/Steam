using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.PricePoint;
using Steam.Application.Services.Store.Interfaces;

namespace Steam.API.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricePointController : ControllerBase
    {
        private readonly IPricePointService _service;

        public PricePointController(IPricePointService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<PricePointListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllPricePointsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PricePointReturnDto>> GetById(int id)
        {
            var result = await _service.GetPricePointByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PricePointReturnDto>> Create([FromBody] PricePointCreateDto dto)
        {
            var result = await _service.CreatePricePointAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PricePointReturnDto>> Update(int id, [FromBody] PricePointUpdateDto dto)
        {
            var result = await _service.UpdatePricePointAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeletePricePointAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
