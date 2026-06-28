using FluentValidation;
using FPTRewardSystem.API.Dtos;

namespace FPTRewardSystem.API.Validator
{
    public class UserSearchQueryValidator : AbstractValidator<UserSearchQueryDto>
    {
        public UserSearchQueryValidator()
        {
            RuleFor(x => x.SearchTerm).MaximumLength(500).WithMessage("Search Term không quá 500 ký tự.");
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
