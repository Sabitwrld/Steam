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

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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

            // Automatically assign the "User" role to every new registration
            await _userManager.AddToRoleAsync(user, "User");

            var token = await _tokenService.GenerateToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1),
                FullName = user.FullName,
                Roles = roles.ToList()
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

            return new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1),
                FullName = user.FullName,
                Roles = roles.ToList()
            };
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // TODO: Implement an EmailService to send the token to the user's email.
            // await _emailService.SendPasswordResetEmailAsync(user.Email, token);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            return result.Succeeded;
        }
    }
}
