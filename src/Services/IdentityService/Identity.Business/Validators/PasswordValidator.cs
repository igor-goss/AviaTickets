using FluentValidation;
using Identity.Business.DTOs;

namespace Identity.Business.Validators
{
    public class PasswordValidator : AbstractValidator<PasswordChangeDTO>
    {
        public PasswordValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty();

            RuleFor(x => x.NewPassword).NotEmpty();

            RuleFor(x => x.NewPassword).NotEqual(x => x.OldPassword);
        }
    }
}
