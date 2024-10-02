using FluentValidation;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Infrastructure.Validator
{
    public class SpecializationValidator : AbstractValidator<Specialization>
    {
        public SpecializationValidator()
        {
        

            RuleFor(specialization => specialization.SpecializationName)
                .NotEmpty().WithMessage("Specialization name is required.")
                .MinimumLength(3).WithMessage("Specialization name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Specialization name must not exceed 100 characters.");

            RuleFor(specialization => specialization.IsActive)
                .NotNull().WithMessage("Specialization status must be specified.");
        }
    }
}
