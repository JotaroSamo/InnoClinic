using Document_API.Domain.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_API.Infrasructure.Validator
{
    public class PhotoValidator : AbstractValidator<Photo>
    {
        public PhotoValidator()
        {
            // Проверка на обязательность свойства Id
            RuleFor(photo => photo.Id)
                .NotEmpty().WithMessage("Id is required.");

            // Проверка на обязательность URL и его формат
            RuleFor(photo => photo.Url)
                .NotEmpty().WithMessage("Url is required.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("Url is not in the correct format.");
        }
    }
}
