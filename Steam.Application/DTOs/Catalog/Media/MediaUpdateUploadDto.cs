using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Catalog.Media
{
    public class MediaUpdateUploadDto
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Url { get; set; } = default!;
        public string MediaType { get; set; } = default!;
        public int Order { get; set; }
        public IFormFile? File { get; set; } // Optional fayl
    }

}
