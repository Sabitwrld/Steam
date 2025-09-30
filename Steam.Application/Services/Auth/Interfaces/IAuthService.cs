using Steam.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
    }
}
