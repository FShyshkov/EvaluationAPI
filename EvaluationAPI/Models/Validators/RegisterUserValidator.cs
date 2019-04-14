using EvaluationAPI.BLL.Requests;
using FluentValidation;

namespace EvaluationAPI.Models.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.FirstName).Length(2, 30).NotEmpty();
            RuleFor(x => x.LastName).Length(2, 30).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.UserName).Length(3, 255).NotEmpty().Matches(@"^[a-zA-Z0-9]+$");
            RuleFor(x => x.Password).Length(6, 15).NotEmpty();
        }
    }
}
