using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class UserTestUpdateValidator : AbstractValidator<UserTestUpdateDto>
    {
        public UserTestUpdateValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertyName} have to be bigger than 0!");
            RuleFor(x => x.UserId).InclusiveBetween(1, int.MaxValue)
                .WithMessage("{PropertyName} have to be bigger than 0!");
        }
    }
}

