using FluentValidation;
using FPTRewardSystem.API.Dtos;

namespace FPTRewardSystem.API.Validator
{
    public class IssuePointsValidator : AbstractValidator<IssuePointsRequestDto>
    {
        public IssuePointsValidator()
        {
            RuleFor(x => x.IssueAmountPerUser)
                .GreaterThan(0).WithMessage("Số lượng điểm cấp phát phải lớn hơn 0.");
        }
    }
}
