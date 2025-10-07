using FluentValidation;
using Steam.Application.DTOs.Achievements.Leaderboard;

namespace Steam.Application.Validators.Achievements.Leaderboard
{
    public class LeaderboardUpdateValidator : AbstractValidator<LeaderboardUpdateDto>
    {
        public LeaderboardUpdateValidator()
        {
            RuleFor(x => x.Score)
                .GreaterThanOrEqualTo(0).WithMessage("Score cannot be negative.");
        }
    }
}
