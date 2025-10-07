using FluentValidation;
using Steam.Application.DTOs.Achievements.UserAchievement;

namespace Steam.Application.Validators.Achievements.UserAchievement
{
    public class UserAchievementCreateValidator : AbstractValidator<UserAchievementCreateDto>
    {
        public UserAchievementCreateValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.AchievementId)
                .GreaterThan(0).WithMessage("A valid AchievementId is required.");
        }
    }
}
