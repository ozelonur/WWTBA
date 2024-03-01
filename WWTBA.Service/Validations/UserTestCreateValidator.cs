using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class UserTestCreateValidator : AbstractValidator<UserTestCreateDto>
    {
        public UserTestCreateValidator()
        {
            RuleFor(x => x.UserId).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertyName} have to be bigger than 0!");
        }
    }
}

