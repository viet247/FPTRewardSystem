using FluentValidation;
using FPTRewardSystem.API.Dtos;

namespace FPTRewardSystem.API.Validator
{
    public class LogoutRequestDtoValidator : AbstractValidator<LogoutRequestDto>
    {
        public LogoutRequestDtoValidator()
        {
            RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token không được để trống.")
            .NotNull().WithMessage("Refresh token không được để null.");
        }
    }
}
