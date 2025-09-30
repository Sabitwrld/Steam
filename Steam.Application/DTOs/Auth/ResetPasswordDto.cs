using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Auth
{
    public record ResetPasswordDto
    {
        public string Email { get; init; } = null!;
        public string Token { get; init; } = null!;
        public string NewPassword { get; init; } = null!;
    }
}
