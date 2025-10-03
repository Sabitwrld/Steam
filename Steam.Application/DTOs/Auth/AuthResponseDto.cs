namespace Steam.Application.DTOs.Auth
{
    public record AuthResponseDto
    {
        public string Token { get; init; } = null!;
        public DateTime Expiration { get; init; }
    }
}
