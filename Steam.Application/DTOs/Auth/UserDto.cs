using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Auth
{
    public record UserDto
    {
        public string Id { get; init; } = default!;
        public string? FullName { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public bool EmailConfirmed { get; init; }
        public DateTime CreatedAt { get; init; }
        public IList<string> Roles { get; set; } = new List<string>(); // CHANGED FROM 'init' TO 'set'
    }
}
