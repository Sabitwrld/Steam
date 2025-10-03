namespace Steam.Application.DTOs.Auth
{
    public record RegisterDto
    {
        public string FullName { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
