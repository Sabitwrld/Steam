using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steam.Application.DTOs.Auth;
using Steam.Application.Services.Auth.Interfaces;
using System.Security.Claims;

namespace Steam.API.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }

        // YENİ ENDPOINT
        [Authorize] // Bu atribut sayəsində yalnız token göndərən istifadəçilər müraciət edə bilər
        [HttpGet("me")]
        public async Task<ActionResult<UserLoginResponseDto>> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var user = await _authService.GetCurrentUserAsync(userId);
            return Ok(user);
        }


        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            // Dəyişiklik yoxdur
            var result = await _authService.ForgotPasswordAsync(dto);
            if (!result) return NotFound("User not found");
            return Ok("Password reset token sent to email");
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            // Dəyişiklik yoxdur
            var result = await _authService.ResetPasswordAsync(dto);
            if (!result) return BadRequest("Invalid token or email");
            return Ok("Password reset successfully");
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok(new { message = "Logout successful" });
        }
    }
}
