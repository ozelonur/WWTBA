using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class UserCreateValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("{PropertyName} is required!")
                .NotEmpty()
                .WithMessage("{PropertyName} is required!")
                .Length(2, 50)
                .WithMessage("{PropertyName} must be between 2 and 50 characters!");

            RuleFor(x => x.Username)
                .NotNull()
                .WithMessage("{PropertyName} is required!")
                .NotEmpty()
                .WithMessage("{PropertyName} is required!")
                .Length(3, 50)
                .WithMessage("{PropertyName} must be between 3 and 50 characters!");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("{PropertyName} is required!")
                .NotEmpty()
                .WithMessage("{PropertyName} is required!")
                .MinimumLength(6)
                .WithMessage("{PropertyName} must be at least 6 characters!");
        }
    }
}