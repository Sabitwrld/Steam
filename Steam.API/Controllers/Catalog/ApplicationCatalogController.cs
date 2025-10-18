using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Catalog.Application;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Catalog.Media;
using Steam.Application.DTOs.Catalog.SystemRequirements;
using Steam.Application.DTOs.Catalog.Tag;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Catalog.Implementations;
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

        // Oyunların/Tətbiqlərin Siyahısı: { "1": {...}, "2": {...} }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("all")] // "api/catalog/all"
        public async Task<IActionResult> GetApplications()
        {
            // 1. Servisdən bütün məlumatları çəkmək üçün GetAllAsync metoduna
            // int.MaxValue ötürülür.
            var pagedApplications = await _service.GetAllAsync(
                pageNumber: 1,
                pageSize: int.MaxValue, // Bütün rekordları çəkmək üçün
                searchTerm: null,
                genreId: null,
                tagId: null);

            // 2. PagedResponse-dan məlumat siyahısı (Data) çıxarılır
            var applications = pagedApplications.Data;

            // 3. List<DTO> obyektini sizin istədiyiniz format olan Dictionary<ID, DTO> formatına çevirir
            var indexedResponse = applications.ToDictionary(app => app.Id, app => app);

            // 4. İndekslənmiş JSON obyekti qaytarılır: { "1": {...}, "2": {...} }
            return Ok(indexedResponse);
        }

        // Səhifələnmiş Siyahı (PagedResponse formatı standart olaraq saxlanıldı)
        [HttpGet] // "api/catalog?pageNumber=..."
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

        // Tək Oyun: { "5": {...} }
        [HttpGet("{id}")] // "api/catalog/5"
        public async Task<ActionResult<Dictionary<int, ApplicationCatalogReturnDto>>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            // Tək DTO-nu ID ilə indekslənmiş obyektdə bükür
            var indexedResponse = new Dictionary<int, ApplicationCatalogReturnDto>
            {
                { result.Id, result }
            };

            return Ok(indexedResponse);
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

        // Janrlar: { "1": {...}, "2": {...} }
        [HttpGet("{id}/genres")]
        public async Task<ActionResult<Dictionary<int, GenreListItemDto>>> GetGenres(int id)
        {
            var genres = await _service.GetGenresByApplicationIdAsync(id);
            var indexedResponse = genres.ToDictionary(g => g.Id, g => g);
            return Ok(indexedResponse);
        }

        // Teqlər: { "1": {...}, "2": {...} }
        [HttpGet("{id}/tags")]
        public async Task<ActionResult<Dictionary<int, TagListItemDto>>> GetTags(int id)
        {
            var tags = await _service.GetTagsByApplicationIdAsync(id);
            var indexedResponse = tags.ToDictionary(t => t.Id, t => t);
            return Ok(indexedResponse);
        }

        // Mediya: { "1": {...}, "2": {...} }
        [HttpGet("{id}/media")]
        public async Task<ActionResult<Dictionary<int, MediaListItemDto>>> GetMedia(int id)
        {
            var media = await _service.GetMediaByApplicationIdAsync(id);
            var indexedResponse = media.ToDictionary(m => m.Id, m => m);
            return Ok(indexedResponse);
        }

        // Sistem tələbləri: { "1": {...}, "2": {...} }
        [HttpGet("{id}/system-requirements")]
        public async Task<ActionResult<Dictionary<int, SystemRequirementsListItemDto>>> GetSystemRequirements(int id)
        {
            var requirements = await _service.GetSystemRequirementsByApplicationIdAsync(id);
            var indexedResponse = requirements.ToDictionary(r => r.Id, r => r);
            return Ok(indexedResponse);
        }
    }

}
