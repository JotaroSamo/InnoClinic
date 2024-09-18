using FluentValidation;
using Profile_API.Domain.Models;

namespace Profile_API.Infrastructure.Validator;

public class PatientValidator : AbstractValidator<Patient>
{
    public PatientValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.MiddleName)
            .NotEmpty().WithMessage("Middle name is required.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage("Date of birth cannot be in the future.");
    }
}