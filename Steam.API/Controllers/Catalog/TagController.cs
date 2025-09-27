using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Catalog.Genre;
using Steam.Application.DTOs.Catalog.Tag;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Catalog.Implementations;
using Steam.Application.Services.Catalog.Interfaces;

namespace Steam.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<TagListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _tagService.GetAllTagAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET: api/tag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TagReturnDto>> GetById(int id)
        {
            var result = await _tagService.GetTagByIdAsync(id);
            return Ok(result);
        }

        // POST: api/tag
        [HttpPost]
        public async Task<ActionResult<TagReturnDto>> Create([FromBody] TagCreateDto dto)
        {
            var result = await _tagService.CreateTagAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT: api/tag/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TagReturnDto>> Update(int id, [FromBody] TagUpdateDto dto)
        {
            var result = await _tagService.UpdateTagAsync(id, dto);
            return Ok(result);
        }

        // DELETE: api/tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _tagService.DeleteTagAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
