using FluentValidation;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Infrastructure.Validator
{
    public class ServiceValidator : AbstractValidator<Service>
    {
        public ServiceValidator()
        {
            RuleFor(service => service.Id).NotEmpty().WithMessage("Service ID cannot be empty.");

            RuleFor(service => service.ServiceName)
                .NotEmpty().WithMessage("Service name is required.")
                .MinimumLength(3).WithMessage("Service name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Service name must not exceed 100 characters.");

            RuleFor(service => service.Price)
                .GreaterThan(0).WithMessage("Service price must be greater than zero.");

            RuleFor(service => service.CategoryId)
                .NotEmpty().WithMessage("Category ID cannot be empty.");

            RuleFor(service => service.SpecializationId)
                .NotEmpty().WithMessage("Specialization ID cannot be empty.");

            RuleFor(service => service.IsActive)
                .NotNull().WithMessage("Service status must be specified.");
        }
    }
}
