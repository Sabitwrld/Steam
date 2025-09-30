using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Auth
{
    public record LoginDto
    {
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
        public bool RememberMe { get; init; }
    }
}
