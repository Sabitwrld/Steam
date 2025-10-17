using Steam.Application.DTOs.Auth;

namespace Steam.Application.Services.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
        Task<UserLoginResponseDto> GetCurrentUserAsync(string userId);
        Task LogoutAsync();
    }
}
