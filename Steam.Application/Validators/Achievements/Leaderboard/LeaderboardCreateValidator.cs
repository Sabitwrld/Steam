using FluentValidation;
using Steam.Application.DTOs.Achievements.Leaderboard;

namespace Steam.Application.Validators.Achievements.Leaderboard
{
    public class LeaderboardCreateValidator : AbstractValidator<LeaderboardCreateDto>
    {
        public LeaderboardCreateValidator()
        {
            RuleFor(x => x.ApplicationId)
                .GreaterThan(0).WithMessage("A valid ApplicationId is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Score)
                .GreaterThanOrEqualTo(0).WithMessage("Score cannot be negative.");
        }
    }
}
