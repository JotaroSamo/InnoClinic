using FluentValidation;
using System.Security.Principal;
using Profile_API.Domain.Models;

namespace Profile_API.Infrastructure.Validator;

public class AccountValidator : AbstractValidator<Account>
{
    public AccountValidator()
    {

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).MaximumLength(16).WithMessage("Password must be at least 6 characters long or then 16.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+\d{10,15}$").WithMessage("Phone number must be in international format.");
       
    }
}