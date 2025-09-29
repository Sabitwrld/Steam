using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Library.License
{
    public record LicenseListItemDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public string LicenseType { get; init; } = default!;
    }
}
