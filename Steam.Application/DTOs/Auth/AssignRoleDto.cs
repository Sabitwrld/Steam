using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.DTOs.Auth
{
    /// <summary>
    /// DTO used by an admin to assign a specific role to a user.
    /// </summary>
    public record AssignRoleDto
    {
        public string UserId { get; init; } = default!;
        public string RoleName { get; init; } = default!;
    }
}
