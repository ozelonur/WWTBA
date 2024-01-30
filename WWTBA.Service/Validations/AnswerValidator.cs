using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class AnswerValidator : AbstractValidator<AnswerDto>
    {
        public AnswerValidator()
        {
            RuleFor(x => x.AnswerText).NotNull().WithName("{PropertyName} is required!").NotEmpty()
                .WithMessage("{PropertyName} is required!");
            RuleFor(x => x.QuestionId).NotNull().WithName("{PropertyName} is required!").NotEmpty()
                .WithMessage("{PropertyName} is required!");
        }
    }
}