namespace Steam.Application.DTOs.Achievements.Leaderboard
{
    public record LeaderboardUpdateDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int Score { get; init; }
    }
}
