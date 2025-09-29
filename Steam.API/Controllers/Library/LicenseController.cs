using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Library.License;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Library.Interfaces;

namespace Steam.API.Controllers.Library
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseService _licenseService;

        public LicenseController(ILicenseService licenseService)
        {
            _licenseService = licenseService;
        }

        // GET: api/License
        [HttpGet]
        public async Task<ActionResult<PagedResponse<LicenseListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _licenseService.GetAllLicensesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/License/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LicenseReturnDto>> GetById(int id)
        {
            var result = await _licenseService.GetLicenseByIdAsync(id);
            return Ok(result);
        }

        // POST: api/License/{libraryId}
        [HttpPost("{libraryId}")]
        public async Task<ActionResult<LicenseReturnDto>> Create(int libraryId, [FromBody] LicenseCreateDto dto)
        {
            var result = await _licenseService.CreateLicenseAsync(libraryId, dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/License/5
        [HttpPut("{id}")]
        public async Task<ActionResult<LicenseReturnDto>> Update(int id, [FromBody] LicenseUpdateDto dto)
        {
            var result = await _licenseService.UpdateLicenseAsync(id, dto);
            return Ok(result);
        }

        // DELETE: api/License/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _licenseService.DeleteLicenseAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
