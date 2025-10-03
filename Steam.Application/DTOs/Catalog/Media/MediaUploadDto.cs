using Microsoft.AspNetCore.Http;

namespace Steam.Application.DTOs.Catalog.Media
{
    public record MediaUploadDto
    {
        public IFormFile File { get; init; } = default!;
        public int ApplicationId { get; init; }
        public string MediaType { get; init; } = default!;
        public int Order { get; init; }
    }

}
