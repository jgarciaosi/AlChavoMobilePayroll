using FluentValidation;

namespace AlChavoMobilePayroll.Models.SManagement.Login
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {



        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name is Required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is Required");
        }
    }

}
