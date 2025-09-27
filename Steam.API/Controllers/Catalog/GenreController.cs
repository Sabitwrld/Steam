using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Catalog.Application;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Catalog.Implementations;
using Steam.Application.Services.Catalog.Interfaces;

namespace Steam.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<GenreListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _genreService.GetAllGenreAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/Genre/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreReturnDto>> GetById(int id)
        {
            var result = await _genreService.GetGenreByIdAsync(id);
            return Ok(result);
        }

        // POST: api/Genre
        [HttpPost]
        public async Task<ActionResult<GenreReturnDto>> Create([FromBody] GenreCreateDto dto)
        {
            var result = await _genreService.CreateGenreAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/Genre/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GenreReturnDto>> Update(int id, [FromBody] GenreUpdateDto dto)
        {
            var result = await _genreService.UpdateGenreAsync(id, dto);
            return Ok(result);
        }

        // DELETE: api/Genre/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _genreService.DeleteGenreAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
