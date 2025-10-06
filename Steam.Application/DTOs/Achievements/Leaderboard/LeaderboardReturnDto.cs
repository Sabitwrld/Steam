namespace Steam.Application.DTOs.Achievements.Leaderboard
{
    /// <summary>
    /// Represents a single entry in a leaderboard, including user details.
    /// </summary>
    public record LeaderboardReturnDto
    {
        public int Id { get; init; }
        public int ApplicationId { get; init; }
        public string UserId { get; init; } = default!;
        public string UserName { get; init; } = default!; // For display
        public int Score { get; init; }
    }
}
