using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Catalog.SystemRequirements;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Catalog.Interfaces;

namespace Steam.API.Controllers.Catalog
{
    [Route("api/system-requirements")]
    [ApiController]
    public class SystemRequirementsController : ControllerBase
    {
        private readonly ISystemRequirementsService _service;

        public SystemRequirementsController(ISystemRequirementsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<SystemRequirementsListItemDto>>> GetAll(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SystemRequirementsReturnDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SystemRequirementsReturnDto>> Create([FromBody] SystemRequirementsCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SystemRequirementsReturnDto>> Update(int id, [FromBody] SystemRequirementsUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

}
