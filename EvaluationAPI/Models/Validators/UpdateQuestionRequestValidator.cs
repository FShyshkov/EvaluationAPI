using FluentValidation;


namespace EvaluationAPI.Models.Validators
{
    public class UpdateQuestionRequestValidator : AbstractValidator<EvaluationAPI.Models.Requests.UpdateQuestionRequest>
    {
        public UpdateQuestionRequestValidator()
        {
            RuleFor(x => x.QuestionId).NotEmpty();
            RuleFor(x => x.TestId).NotEmpty();
            RuleFor(x => x.PossibleAnswers).NotEmpty();
            RuleFor(x => x.QuestionText).NotEmpty();
            RuleFor(x => x.correctAnswers).NotEmpty();
        }
    }
}
