namespace Steam.Application.DTOs.Auth
{
    public record ForgotPasswordDto
    {
        public string Email { get; init; } = null!;
    }
}
