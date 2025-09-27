using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Catalog.Media;
using Steam.Application.Services.Catalog.Interfaces;

namespace Steam.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly IWebHostEnvironment _env;

        public MediaController(IMediaService mediaService, IWebHostEnvironment env)
        {
            _mediaService = mediaService;
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }


        // CREATE / UPLOAD
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadMedia([FromForm] MediaUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("No file uploaded.");

            var webRoot = _env.WebRootPath;
            if (string.IsNullOrEmpty(webRoot))
                webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var uploadsFolder = Path.Combine(webRoot, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            var mediaDto = new MediaCreateDto
            {
                ApplicationId = dto.ApplicationId,
                Url = $"/uploads/{fileName}",
                MediaType = dto.MediaType,
                Order = dto.Order
            };

            var result = await _mediaService.CreateMediaAsync(mediaDto);

            return Ok(result);
        }

        // READ ALL
        [HttpGet]
        public async Task<IActionResult> GetAllMedia(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _mediaService.GetAllMediaAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // READ BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMediaById(int id)
        {
            var result = await _mediaService.GetMediaByIdAsync(id);
            if (result == null)
                return NotFound($"Media with Id {id} not found.");

            return Ok(result);
        }
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateMedia(int id, [FromForm] MediaUpdateUploadDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id mismatch.");

            // Fayl varsa upload et
            string? fileUrl = null;
            if (dto.File != null && dto.File.Length > 0)
            {
                var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                var uploadsFolder = Path.Combine(webRoot, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.File.CopyToAsync(stream);
                }

                fileUrl = $"/uploads/{fileName}";
            }

            var mediaDto = new MediaUpdateDto
            {
                Id = dto.Id,
                ApplicationId = dto.ApplicationId,
                Url = fileUrl ?? dto.Url, // Fayl yüklənməsə əvvəlki URL qalır
                MediaType = dto.MediaType,
                Order = dto.Order
            };

            var result = await _mediaService.UpdateMediaAsync(id, mediaDto);
            return Ok(result);
        }


        // DELETE (default olaraq soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedia(int id)
        {
            var deleted = await _mediaService.DeleteMediaAsync(id); // soft delete default
            if (!deleted)
                return NotFound($"Media with Id {id} not found.");

            return NoContent();
        }



    }
}
