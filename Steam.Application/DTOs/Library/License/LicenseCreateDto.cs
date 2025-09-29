using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Library.License
{
    public record LicenseCreateDto
    {
        public int ApplicationId { get; init; }
        public string LicenseType { get; init; } = default!;
        public DateTime? ExpirationDate { get; init; }
    }
}
