using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Catalog.Application;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Catalog.Interfaces;
using Steam.Infrastructure.Repositories.Interfaces.Catalog;

namespace Steam.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationCatalogController : ControllerBase
    {
        private readonly IApplicationCatalogService _applicationCatalogService;

        public ApplicationCatalogController(IApplicationCatalogService applicationCatalogService)
        {
            _applicationCatalogService = applicationCatalogService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<ApplicationCatalogListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _applicationCatalogService.GetAllApplicationCatalogAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/ApplicationCatalog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationCatalogReturnDto>> GetById(int id)
        {
            var result = await _applicationCatalogService.GetApplicationCatalogByIdAsync(id);
            return Ok(result);
        }

        // POST: api/ApplicationCatalog
        [HttpPost]
        public async Task<ActionResult<ApplicationCatalogReturnDto>> Create([FromBody] ApplicationCatalogCreateDto dto)
        {
            var result = await _applicationCatalogService.CreateApplicationCatalogAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/ApplicationCatalog/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationCatalogReturnDto>> Update(int id, [FromBody] ApplicationCatalogUpdateDto dto)
        {
            var result = await _applicationCatalogService.UpdateApplicationCatalogAsync(id, dto);
            return Ok(result);
        }

        // DELETE: api/ApplicationCatalog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _applicationCatalogService.DeleteApplicationCatalogAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
