using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class UserAnswerCreateValidator : AbstractValidator<UserAnswerCreateDto>
    {
        public UserAnswerCreateValidator()
        {
            RuleFor(x => x.UserId).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertyName have to be greater than 0!}");
            RuleFor(x => x.AnswerId).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertyName have to be greater than 0!}");
            RuleFor(x => x.QuestionSolveTime).InclusiveBetween(.01f, float.MaxValue)
                .WithMessage("{PropertyName have to be greater than 0!}");
        }
    }
}