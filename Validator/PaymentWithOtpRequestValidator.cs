using FluentValidation;
using FPTRewardSystem.API.Dtos;

namespace FPTRewardSystem.API.Validator
{
    public class PaymentWithOtpRequestValidator : AbstractValidator<PaymentWithOtpRequestDto>
    {
        public PaymentWithOtpRequestValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId không được để trống");
            // Otp không được trống, 6 ký tự, chỉ chứa số
            RuleFor(x => x.Otp).NotEmpty().WithMessage("Otp không được để trống")
                .Length(6).WithMessage("Mã Otp phải đúng 6 chữ số")
                .Matches(@"^[0-9]+$").WithMessage("Mã Otp chỉ được chứa ký tự số");
            // MerchantId không được để trống
            RuleFor(x => x.MerchantId).NotEmpty().WithMessage("MerchantId không được để trống");
            // Amount phải lớn hơn 0
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Số lượng điểm cần chuyển phải lớn hơn 0");
        }
    }
}
