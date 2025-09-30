using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Auth
{
    public record AuthResponseDto
    {
        public string Token { get; init; } = null!;
        public DateTime Expiration { get; init; }
    }
}
