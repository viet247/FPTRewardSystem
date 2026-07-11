using FluentValidation;
using FPTRewardSystem.API.Dtos;

namespace FPTRewardSystem.API.Validator
{
    public class TransactionRequestValidator : AbstractValidator<TransactionRequestDto>
    {
        public TransactionRequestValidator()
        {
            // 1. Ràng buộc cho Amount
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount phải lớn hơn 0.");
            // 2. Ràng buộc cho description
            RuleFor(x => x.Description)
                .MaximumLength(50).WithMessage("Description không được vượt quá 50 ký tự.");
        }
    }
}
