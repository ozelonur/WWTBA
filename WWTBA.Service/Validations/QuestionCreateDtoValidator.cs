using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class QuestionCreateDtoValidator : AbstractValidator<QuestionCreateDto>
    {
        public QuestionCreateDtoValidator()
        {
            RuleFor(x => x.SubjectId).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertyName} have to be greater than 0!");
            RuleFor(x => x.QuestionText).NotNull().WithMessage("{PropertyName} is required!").NotEmpty()
                .WithMessage("{PropertyName} is required!");
        }
    }
}