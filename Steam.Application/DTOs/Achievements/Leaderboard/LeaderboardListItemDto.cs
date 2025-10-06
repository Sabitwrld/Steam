namespace Steam.Application.DTOs.Achievements.Leaderboard
{
    /// <summary>
    /// Represents a summarized leaderboard entry, suitable for a ranked list.
    /// </summary>
    public record LeaderboardListItemDto
    {
        public int Rank { get; set; } // This will be set in the service layer
        public string UserName { get; init; } = default!;
        public int Score { get; init; }
    }
}
