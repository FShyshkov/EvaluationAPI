using EvaluationAPI.BLL.Requests;
using FluentValidation;


namespace EvaluationAPI.Models.Validators
{
    public class RequestValidator: AbstractValidator<EvaluationAPI.Models.Requests.ResultRequest>
    {
        public RequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().Length(3, 255);
            RuleFor(x => x.UserResult).NotEmpty().LessThanOrEqualTo(100).GreaterThanOrEqualTo(0);
        }
    }
}
