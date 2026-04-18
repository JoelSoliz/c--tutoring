using FluentValidation;
using WatchPartyAPI.DTOs.Requests;

namespace WatchPartyAPI.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(user => user.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(2).WithMessage("Username must be at least 2 characters")
                .MaximumLength(50).WithMessage("Username must be at most 50 characters");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number");
        }
    }
}
