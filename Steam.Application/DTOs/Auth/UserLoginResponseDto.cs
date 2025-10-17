using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Auth
{
    /// <summary>
    /// Login və Register sonrası frontend-ə göndərilən istifadəçi məlumatlarını saxlayır.
    /// </summary>
    public record UserLoginResponseDto
    {
        public string Id { get; init; } = null!;
        public string? FullName { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public List<string> Roles { get; init; } = new();
    }
}
