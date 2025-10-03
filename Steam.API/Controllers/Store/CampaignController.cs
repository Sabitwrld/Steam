using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Campaign;
using Steam.Application.Services.Store.Interfaces;

namespace Steam.API.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _service;

        public CampaignController(ICampaignService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<CampaignListItemDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetAllCampaignsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignReturnDto>> GetById(int id)
        {
            var result = await _service.GetCampaignByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CampaignReturnDto>> Create([FromBody] CampaignCreateDto dto)
        {
            var result = await _service.CreateCampaignAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CampaignReturnDto>> Update(int id, [FromBody] CampaignUpdateDto dto)
        {
            var result = await _service.UpdateCampaignAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteCampaignAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
