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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LicenseReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<LicenseReturnDto>> GetById(int id)
        {
            var result = await _licenseService.GetLicenseByIdAsync(id);
            return Ok(result);
        }

        // This endpoint is for adding a new game to a specific library.
        // It would typically be called internally by the OrderService after a successful purchase.
        [HttpPost("library/{userLibraryId}")]
        [ProducesResponseType(typeof(LicenseReturnDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<LicenseReturnDto>> AddLicenseToLibrary(int userLibraryId, [FromBody] LicenseCreateDto dto)
        {
            var result = await _licenseService.AddLicenseAsync(userLibraryId, dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // This endpoint is for updating license details, e.g., playtime or hiding the game.
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(LicenseReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<LicenseReturnDto>> Update(int id, [FromBody] LicenseUpdateDto dto)
        {
            var result = await _licenseService.UpdateLicenseAsync(id, dto);
            return Ok(result);
        }

        // This endpoint would typically be for admin use to revoke a license.
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _licenseService.DeleteLicenseAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
