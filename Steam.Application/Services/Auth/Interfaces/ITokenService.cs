using Steam.Domain.Entities.Identity;

namespace Steam.Application.Services.Auth.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// Generates a JWT token for the specified user, including their roles.
        /// </summary>
        /// <param name="user">The user for whom the token will be generated.</param>
        /// <returns>A JWT token string.</returns>
        Task<string> GenerateToken(AppUser user);
    }
}
