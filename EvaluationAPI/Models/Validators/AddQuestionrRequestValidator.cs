using EvaluationAPI.BLL.Requests;
using FluentValidation;


namespace EvaluationAPI.Models.Validators
{
    public class AddQuestionrRequestValidator : AbstractValidator<EvaluationAPI.Models.Requests.AddQuestionRequest>
    {
        public AddQuestionrRequestValidator()
        {
            RuleFor(x => x.QuestionText).NotEmpty().MinimumLength(3);
            RuleFor(x => x.PossibleAnswers).NotEmpty();
            RuleFor(x => x.correctAnswers).NotEmpty();
        }
    }
}
