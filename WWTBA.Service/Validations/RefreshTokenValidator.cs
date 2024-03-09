using FluentValidation;
using WWTBA.Core.DTOs;

namespace WWTBA.Service.Validations
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenValidator()
        {
            RuleFor(rt => rt.Token)
                .NotEmpty().WithMessage("Token is required.")
                .Length(1, 256).WithMessage("Token must be between 1 and 256 characters.");

            RuleFor(rt => rt.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");

            RuleFor(rt => rt.Created)
                .NotEmpty().WithMessage("Created date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Created date cannot be in the future.");

            RuleFor(rt => rt.Expires)
                .NotEmpty().WithMessage("Expiration date is required.")
                .GreaterThan(rt => rt.Created).WithMessage("Expiration date must be after the created date.");

            RuleFor(rt => rt.IsRevoked)
                .NotNull().WithMessage("IsRevoked field is required.");

            When(rt => rt.Revoked.HasValue, () =>
            {
                RuleFor(rt => rt.Revoked.Value)
                    .GreaterThan(rt => rt.Created).WithMessage("Revoked date must be after the created date.")
                    .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Revoked date cannot be in the future.");
            });
        }
    }
}