using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class AnswerCreateValidator : AbstractValidator<AnswerCreateDto>
    {
        public AnswerCreateValidator()
        {
            RuleFor(x => x.QuestionId).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertyName} have to be greater than 0!");
            RuleFor(x => x.AnswerText).NotNull().WithMessage("{PropertyName} is required!").NotEmpty()
                .WithMessage("{PropertyName} is required!");
            RuleFor(x => x.IsCorrect).Must(x => x == false || x == true)
                .WithMessage("{PropertyName} have to be true or false!");
        }
    }
}

