using Steam.Application.DTOs.Library.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Library.UserLibrary
{
    public record UserLibraryReturnDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public List<LicenseReturnDto> Licenses { get; init; } = new();
    }
}
