using Steam.Domain.Entities.Identity;

namespace Steam.Application.Services.Auth.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user);
    }
}
