namespace Steam.Application.DTOs.Auth
{

    /// <summary>
    /// Represents the response after a successful authentication (login or register).
    /// Contains the JWT token and basic user information for the client.
    /// </summary>
    public record AuthResponseDto
    {
        public string Id { get; init; } = null!;
        public string Token { get; init; } = null!;
        public DateTime Expiration { get; init; }
        public string? FullName { get; init; }
        public List<string> Roles { get; init; } = new();
    }
}