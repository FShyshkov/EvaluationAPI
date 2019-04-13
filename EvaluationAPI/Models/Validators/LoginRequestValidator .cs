using EvaluationAPI.BLL.Requests;
using FluentValidation;

namespace EvaluationAPI.Models.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().Matches(@"^[a-zA-Z0-9]+$");
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
