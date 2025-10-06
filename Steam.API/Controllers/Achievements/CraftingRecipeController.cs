using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Achievements.CraftingRecipe;
using Steam.Application.DTOs.Pagination;
using Steam.Application.Services.Achievements.Interfaces;

namespace Steam.API.Controllers.Achievements
{
    [Route("api/crafting-recipes")]
    [ApiController]
    public class CraftingRecipeController : ControllerBase
    {
        private readonly ICraftingRecipeService _service;

        public CraftingRecipeController(ICraftingRecipeService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<CraftingRecipeListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<CraftingRecipeListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllCraftingRecipesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CraftingRecipeReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CraftingRecipeReturnDto>> GetById(int id)
        {
            var result = await _service.GetCraftingRecipeByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CraftingRecipeReturnDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CraftingRecipeReturnDto>> Create([FromBody] CraftingRecipeCreateDto dto)
        {
            var result = await _service.CreateCraftingRecipeAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CraftingRecipeReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CraftingRecipeReturnDto>> Update(int id, [FromBody] CraftingRecipeUpdateDto dto)
        {
            var result = await _service.UpdateCraftingRecipeAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteCraftingRecipeAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
