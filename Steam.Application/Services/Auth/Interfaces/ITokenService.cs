using Steam.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Auth.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user);
    }
}
