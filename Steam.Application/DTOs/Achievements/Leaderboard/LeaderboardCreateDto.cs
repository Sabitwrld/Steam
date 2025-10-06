namespace Steam.Application.DTOs.Achievements.Leaderboard
{
    /// <summary>
    /// Represents the data needed to add or update a user's score on a leaderboard.
    /// </summary>
    public record LeaderboardCreateDto
    {
        public int ApplicationId { get; init; }
        public string UserId { get; init; } = default!;
        public int Score { get; init; }
    }
}
