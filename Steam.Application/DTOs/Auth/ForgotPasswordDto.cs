using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Auth
{
    public record ForgotPasswordDto
    {
        public string Email { get; init; } = null!;
    }
}
