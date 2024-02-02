using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class SubjectUpdateDtoValidator : AbstractValidator<SubjectUpdateDto>
    {
        public SubjectUpdateDtoValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertyName} have to be greater than 0!");
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required!").NotEmpty()
                .WithMessage("{PropertyName} is required!");
            RuleFor(x => x.LessonId).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertyName} have to be greater than 0!");
        }
    }
}

