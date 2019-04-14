using EvaluationAPI.BLL.Requests;
using FluentValidation;


namespace EvaluationAPI.Models.Validators
{
    public class AddQuestionRequestValidator : AbstractValidator<EvaluationAPI.Models.Requests.AddQuestionRequest>
    {
        public AddQuestionRequestValidator()
        {
            RuleFor(x => x.QuestionText).NotEmpty().MinimumLength(3);
            RuleFor(x => x.PossibleAnswers).NotEmpty();
            RuleFor(x => x.correctAnswers).NotEmpty();
        }
    }
}
