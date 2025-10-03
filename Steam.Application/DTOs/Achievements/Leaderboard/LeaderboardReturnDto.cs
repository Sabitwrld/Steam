namespace Steam.Application.DTOs.Achievements.Leaderboard
{
    public record LeaderboardReturnDto
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public int Score { get; init; }
    }
}
