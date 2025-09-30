using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Steam.Application.DTOs.Auth;
using Steam.Application.Services.Auth.Interfaces;
using Steam.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steam.Application.Services.Auth.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService; // JWT yaratmaq üçün

        public AuthService(UserManager<AppUser> userManager,
                           SignInManager<AppUser> signInManager,
                           IMapper mapper,
                           ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var user = new AppUser
            {
                FullName = dto.FullName,
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors));

            var token = _tokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                throw new Exception("User not found");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                throw new Exception("Invalid credentials");

            var token = _tokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // tokeni email ilə göndərmək burda qalır (sən email service əlavə etməlisən)
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return false;

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            return result.Succeeded;
        }
    }
}
