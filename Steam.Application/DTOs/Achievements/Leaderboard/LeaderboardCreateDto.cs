namespace Steam.Application.DTOs.Achievements.Leaderboard
{
    public record LeaderboardCreateDto
    {
        public int UserId { get; init; }
        public int Score { get; init; }
    }
}
