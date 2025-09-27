using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
