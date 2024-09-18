using FluentValidation;
using Profile_API.Domain.Models;

namespace Profile_API.Infrastructure.Validator;

public class ReceptionistValidator : AbstractValidator<Receptionist>
{
    public ReceptionistValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.MiddleName)
            .NotEmpty().WithMessage("Middle name is required.");

        RuleFor(x => x.OfficeAddress)
            .NotEmpty().WithMessage("Office address is required.");

        RuleFor(x => x.OfficeRegistryPhoneNumber)
            .NotEmpty().WithMessage("Office registry phone number is required.")
            .Matches(@"^\+\d{10,15}$").WithMessage("Phone number must be in international format.");
    }
}