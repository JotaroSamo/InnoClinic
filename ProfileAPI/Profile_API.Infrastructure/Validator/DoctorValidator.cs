using FluentValidation;
using Profile_API.Domain.Models;

namespace Profile_API.Infrastructure.Validator;

public class DoctorValidator : AbstractValidator<Doctor>
{
    public DoctorValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.MiddleName)
            .NotEmpty().WithMessage("Middle name is required.");

        RuleFor(x => x.SpecializationName)
            .NotEmpty().WithMessage("Specialization name is required.");


        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.");

    }
}