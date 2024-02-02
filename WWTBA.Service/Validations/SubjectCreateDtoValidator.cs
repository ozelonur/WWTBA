using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class SubjectCreateDtoValidator : AbstractValidator<SubjectCreateDto>
    {
        public SubjectCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required!").NotEmpty()
                .WithMessage("{PropertyName} is required!");
            RuleFor(x => x.LessonId).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertyName} have to be greater than 0!");
        }
    }
}