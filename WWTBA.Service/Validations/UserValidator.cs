using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("{PropertyName} is required!").NotEmpty()
                .WithMessage("{PropertyName} is required!");
        }
    }
}

