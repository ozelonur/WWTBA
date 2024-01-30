using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class LessonValidator : AbstractValidator<LessonDto>
    {
        public LessonValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required!").NotEmpty()
                .WithMessage("{PropertyName} is required!");
        }
    }
}

