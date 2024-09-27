using FluentValidation;
using Service_API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Infrastructure.Validator
{
    public class ServiceCategoryValidator : AbstractValidator<ServiceCategory>
    {
        public ServiceCategoryValidator()
        {
         

            RuleFor(category => category.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MinimumLength(3).WithMessage("Category name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

            RuleFor(category => category.TimeSlotSize)
                .GreaterThan(0).WithMessage("Time slot size must be greater than zero.");
        }
    }
}
