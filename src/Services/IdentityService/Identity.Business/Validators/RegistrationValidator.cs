using FluentValidation;
using Identity.Business.DTOs;

namespace Identity.Business.Validators
{
    public class RegistrationValidator : AbstractValidator<SignUpDTO>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);

            RuleFor(x => x.Email).MinimumLength(6).MaximumLength(50);

            RuleFor(x => x.Password).NotEmpty();

            RuleFor(x => x.ConfirmPassword).NotEmpty();

            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword);
        }
    }
}
