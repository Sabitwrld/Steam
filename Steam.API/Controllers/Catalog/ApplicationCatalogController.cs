using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Catalog.Application;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Catalog.Media;
using Steam.Application.DTOs.Catalog.SystemRequirements;
using Steam.Application.DTOs.Catalog.Tag;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Catalog.Interfaces;

namespace Steam.API.Controllers.Catalog
{
    [Route("api/catalog")]
    [ApiController]
    public class ApplicationCatalogController : ControllerBase
    {
        private readonly IApplicationCatalogService _service;

        public ApplicationCatalogController(IApplicationCatalogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<ApplicationCatalogListItemDto>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? genreId = null,
        [FromQuery] int? tagId = null)
        {
            var result = await _service.GetAllAsync(pageNumber, pageSize, searchTerm, genreId, tagId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationCatalogReturnDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // PROTECTED
        public async Task<ActionResult<ApplicationCatalogReturnDto>> Create([FromBody] ApplicationCatalogCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // PROTECTED
        public async Task<ActionResult<ApplicationCatalogReturnDto>> Update(int id, [FromBody] ApplicationCatalogUpdateDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // PROTECTED
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}/genres")]
        public async Task<ActionResult<List<GenreListItemDto>>> GetGenres(int id)
        {
            var genres = await _service.GetGenresByApplicationIdAsync(id);
            return Ok(genres);
        }

        // YENİ ENDPOINT: Teqlər üçün
        [HttpGet("{id}/tags")]
        public async Task<ActionResult<List<TagListItemDto>>> GetTags(int id)
        {
            var tags = await _service.GetTagsByApplicationIdAsync(id);
            return Ok(tags);
        }

        // YENİ ENDPOINT: Media üçün
        [HttpGet("{id}/media")]
        public async Task<ActionResult<List<MediaListItemDto>>> GetMedia(int id)
        {
            var media = await _service.GetMediaByApplicationIdAsync(id);
            return Ok(media);
        }

        // YENİ ENDPOINT: Sistem tələbləri üçün
        [HttpGet("{id}/system-requirements")]
        public async Task<ActionResult<List<SystemRequirementsListItemDto>>> GetSystemRequirements(int id)
        {
            var requirements = await _service.GetSystemRequirementsByApplicationIdAsync(id);
            return Ok(requirements);
        }
    }

}
