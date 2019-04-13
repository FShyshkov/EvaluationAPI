using EvaluationAPI.BLL.Requests;
using FluentValidation;


namespace EvaluationAPI.Models.Validators
{
    public class UpdateQuestionRequestValidator : AbstractValidator<EvaluationAPI.Models.Requests.UpdateQuestionRequest>
    {
        public UpdateQuestionRequestValidator()
        {
            RuleFor(x => x.QuestionId).NotEmpty();
            RuleFor(x => x.TestId).NotEmpty();
        }
    }
}
