using FluentValidation;
using Steam.Application.DTOs.Achievements.Leaderboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
