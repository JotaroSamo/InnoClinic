using Document_API.Domain.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_API.Infrasructure.Validator
{
    

    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            // Проверка на обязательность свойства Id
            RuleFor(document => document.Id)
                .NotEmpty().WithMessage("Id is required.");

            // Проверка на обязательность URL и его формат
            RuleFor(document => document.Url)
                .NotEmpty().WithMessage("Url is required.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("Url is not in the correct format.");

            // Проверка на обязательность свойства ResultId
            RuleFor(document => document.ResultId)
                .NotEmpty().WithMessage("ResultId is required.");

            // Проверка максимальной длины для Complaints
            RuleFor(document => document.Complaints)
                .MaximumLength(1000).WithMessage("Complaints should not exceed 1000 characters.");

            // Проверка максимальной длины для Conclusion
            RuleFor(document => document.Conclusion)
                .MaximumLength(1000).WithMessage("Conclusion should not exceed 1000 characters.");

            // Проверка максимальной длины для Recommendations
            RuleFor(document => document.Recommendations)
                .MaximumLength(1000).WithMessage("Recommendations should not exceed 1000 characters.");
        }
    }

}
