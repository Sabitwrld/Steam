using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Pagination;
using Steam.Application.DTOs.Store.Gift;
using Steam.Application.Services.Store.Interfaces;
using Steam.Domain.Entities.Identity;
using System.Security.Claims;

namespace Steam.API.Controllers.Store
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiftController : ControllerBase
    {
        private readonly IGiftService _service;
        private readonly UserManager<AppUser> _userManager;

        public GiftController(IGiftService service, UserManager<AppUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpPost("send")]
        [Authorize]
        [ProducesResponseType(typeof(GiftReturnDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<GiftReturnDto>> SendGift([FromBody] GiftCreateForUserDto dto)
        {
            // Göndərənin ID-sini token-dən götürürük
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(senderId))
            {
                return Unauthorized();
            }

            // Hədiyyə alacaq istifadəçini istifadəçi adına görə tapırıq
            var receiverUser = await _userManager.FindByNameAsync(dto.ReceiverUsername);
            if (receiverUser == null)
            {
                return NotFound($"User with username '{dto.ReceiverUsername}' not found.");
            }

            // Servisin gözlədiyi orijinal DTO-nu yaradırıq (ID-lər ilə)
            var serviceDto = new GiftCreateDto
            {
                SenderId = senderId,
                ReceiverId = receiverUser.Id,
                ApplicationId = dto.ApplicationId
            };

            // Servisi çağırırıq
            var result = await _service.SendGiftAsync(serviceDto);
            return CreatedAtAction(nameof(GetGiftById), new { id = result.Id }, result);
        }

        [HttpPost("{id}/redeem")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RedeemGift(int id)
        {
            // ReceiverId təhlükəsiz şəkildə token-dən oxunur
            var receiverId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(receiverId))
            {
                return Unauthorized();
            }

            await _service.RedeemGiftAsync(id, receiverId);
            return Ok("Gift successfully redeemed and added to your library.");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GiftReturnDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<GiftReturnDto>> GetGiftById(int id)
        {
            var result = await _service.GetGiftByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(PagedResponse<GiftListItemDto>), 200)]
        public async Task<ActionResult<PagedResponse<GiftListItemDto>>> GetGiftsForUser(string userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetGiftsForUserAsync(userId, pageNumber, pageSize);
            return Ok(result);
        }
    }
}
