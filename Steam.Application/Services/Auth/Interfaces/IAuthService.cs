using Steam.Application.DTOs.Auth;

namespace Steam.Application.Services.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto);

        /// <summary>
        /// Token-ə əsasən cari istifadəçinin məlumatlarını qaytarır.
        /// </summary>
        /// <param name="userId">Token-dən oxunmuş istifadəçi ID-si.</param>
        /// <returns>İstifadəçi məlumatlarını saxlayan DTO.</returns>
        Task<UserLoginResponseDto> GetCurrentUserAsync(string userId);
    }
}
