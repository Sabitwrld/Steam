using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Catalog.SystemRequirements;
using Steam.Application.DTOs.Catalog.Tag;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Catalog.Implementations;
using Steam.Application.Services.Catalog.Interfaces;

namespace Steam.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemRequirementsController : ControllerBase
    {
        private readonly ISystemRequirementsService _systemRequirementsService;

        public SystemRequirementsController(ISystemRequirementsService systemRequirementsService)
        {
            _systemRequirementsService = systemRequirementsService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<SystemRequirementsListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _systemRequirementsService.GetAllSystemRequirementsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/systemRequirements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemRequirementsReturnDto>> GetById(int id)
        {
            var result = await _systemRequirementsService.GetSystemRequirementsByIdAsync(id);
            return Ok(result);
        }

        // POST: api/systemRequirements
        [HttpPost]
        public async Task<ActionResult<SystemRequirementsReturnDto>> Create([FromBody] SystemRequirementsCreateDto dto)
        {
            var result = await _systemRequirementsService.CreateSystemRequirementsAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/SystemRequirements/5
        [HttpPut("{id}")]
        public async Task<ActionResult<SystemRequirementsReturnDto>> Update(int id, [FromBody] SystemRequirementsUpdateDto dto)
        {
            var result = await _systemRequirementsService.UpdateSystemRequirementsAsync(id, dto);
            return Ok(result);
        }

        // DELETE: api/SystemRequirements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _systemRequirementsService.DeleteSystemRequirementsAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
