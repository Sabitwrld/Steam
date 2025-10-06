namespace Steam.Application.DTOs.Achievements.Leaderboard
{
    /// <summary>
    /// Represents the data for updating a user's score.
    /// </summary>
    public record LeaderboardUpdateDto
    {
        public int Score { get; init; }
    }
}
