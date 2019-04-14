using EvaluationAPI.BLL.Requests;
using FluentValidation;


namespace EvaluationAPI.Models.Validators
{
    public class TestRequestValidator : AbstractValidator<EvaluationAPI.Models.Requests.TestRequest>
    {
        public TestRequestValidator()
        {
            RuleFor(x => x.TestName).NotEmpty().Length(3, 255).Matches(@"^[a-zA-Z0-9]+$"); ;
        }
    }
}
