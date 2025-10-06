namespace Steam.Application.DTOs.Auth
{

    /// <summary>
    /// Represents the response after a successful authentication (login or register).
    /// Contains the JWT token and basic user information for the client.
    /// </summary>
    public record AuthResponseDto
    {
        /// <summary>
        /// The generated JWT token.
        /// </summary>
        public string Token { get; init; } = null!;

        /// <summary>
        /// The expiration date of the token.
        /// </summary>
        public DateTime Expiration { get; init; }

        /// <summary>
        /// The full name of the authenticated user.
        /// </summary>
        public string? FullName { get; init; }

        /// <summary>
        /// A list of roles the user belongs to (e.g., "Admin", "User").
        /// </summary>
        public List<string> Roles { get; init; } = new();
    }
}