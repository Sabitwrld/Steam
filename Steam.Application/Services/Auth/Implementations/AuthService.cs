using Microsoft.AspNetCore.Identity;
using Steam.Application.DTOs.Auth;
using Steam.Application.Exceptions;
using Steam.Application.Services.Auth.Interfaces;
using Steam.Domain.Entities.Identity;

namespace Steam.Application.Services.Auth.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }

            var user = new AppUser { FullName = dto.FullName, UserName = dto.Email, Email = dto.Email, CreatedAt = DateTime.UtcNow };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, "User");

            var token = await _tokenService.GenerateToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            // YENİ CAVAB STRUKTURU
            return new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1),
                User = new UserLoginResponseDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                }
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new NotFoundException("Invalid email or password.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                throw new Exception("Invalid credentials");

            var token = await _tokenService.GenerateToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            // YENİ CAVAB STRUKTURU
            return new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1),
                User = new UserLoginResponseDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                }
            };
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            // Bu metodda dəyişiklik yoxdur
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://your-frontend-url/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(user.Email)}";
            await _emailService.SendEmailAsync(user.Email, "Reset Your Password", $"Please reset your password by clicking here: <a href='{resetLink}'>link</a>");
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            // Bu metodda dəyişiklik yoxdur
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;
            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            return result.Succeeded;
        }

        // YENİ METOD
        public async Task<UserLoginResponseDto> GetCurrentUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new NotFoundException(nameof(AppUser), userId);

            var roles = await _userManager.GetRolesAsync(user);

            return new UserLoginResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList()
            };
        }
        public async Task LogoutAsync()
        {
            // Bu metod ASP.NET Core Identity-nin daxili autentikasiya cookie-lərini təmizləyir.
            // JWT ilə birbaşa əlaqəsi olmasa da, təhlükəsiz çıxışı təmin etmək üçün yaxşı təcrübədir.
            // Əsas məntiq frontend-də token-in silinməsidir.
            await _signInManager.SignOutAsync();
        }
    }
}
