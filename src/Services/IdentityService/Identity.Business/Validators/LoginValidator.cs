using FluentValidation;
using Identity.Business.DTOs;

namespace Identity.Business.Validators
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator() 
        {
            RuleFor(x => x.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);

            RuleFor(x => x.Password).NotEmpty();

            RuleFor(x => x.Email).MinimumLength(6).MaximumLength(50);
        }
    }
}
