using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class LessonUpdateDtoValidator : AbstractValidator<LessonUpdateDto>
    {
        public LessonUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required!").NotEmpty()
                .WithMessage("{PropertyName} is required!");
        }
    }
}