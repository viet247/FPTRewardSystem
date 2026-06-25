using FluentValidation;
using FPTRewardSystem.API.Dtos;

namespace FPTRewardSystem.API.Validator
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequestDto>
    {
        public CreateUserRequestValidator()
        {
            // 1. Ràng buộc cho FullName
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ và tên không được để trống.")
                .Length(2, 100).WithMessage("Họ và tên phải từ 2 đến 100 ký tự.");

            // 2. Ràng buộc cho Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống.")
                .EmailAddress().WithMessage("Định dạng Email không hợp lệ.")
                .MaximumLength(256).WithMessage("Email không được vượt quá 256 ký tự.");

            // 3. Ràng buộc bảo mật cho Password
            RuleFor(x => x.PassWord)
                .NotEmpty().WithMessage("Mật khẩu không được để trống.")
                .MinimumLength(8).WithMessage("Mật khẩu phải có ít nhất 8 ký tự.")
                .MaximumLength(50).WithMessage("Mật khẩu không được vượt quá 50 ký tự.")
                .Matches(@"[A-Z]").WithMessage("Mật khẩu phải chứa ít nhất 1 chữ cái viết hoa.")
                .Matches(@"[a-z]").WithMessage("Mật khẩu phải chứa ít nhất 1 chữ cái viết thường.")
                .Matches(@"[0-9]").WithMessage("Mật khẩu phải chứa ít nhất 1 chữ số.")
                .Matches(@"[\^$*.\[\]{}()?""!@#%&/\\,><':;|_~`\-+=]").WithMessage("Mật khẩu phải chứa ít nhất 1 ký tự đặc biệt.");
        }
    }
}
