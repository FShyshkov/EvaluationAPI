using EvaluationAPI.BLL.Requests;
using FluentValidation;


namespace EvaluationAPI.Models.Validators
{
    public class TestRequestValidator : AbstractValidator<EvaluationAPI.Models.Requests.TestRequest>
    {
        public TestRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().Length(3, 255);
        }
    }
}
