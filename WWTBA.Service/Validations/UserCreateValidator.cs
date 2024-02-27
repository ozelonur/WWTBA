using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class UserCreateValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("{PropertyName} is required!").NotEmpty()
                .WithMessage("{PropertyName} is required!");
            RuleFor(x => x.UniqueIdentifier).NotNull().WithMessage("{PropertyName} is required!").NotEmpty()
                .WithMessage("{PropertyName} is required!");
        }
    }
}

