using Appointment_API.Domain.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.Infrastructure.Validator
{

    public class ResultValidator : AbstractValidator<Results>
    {
        public ResultValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Идентификатор результата не должен быть пустым.");

            RuleFor(x => x.Complaints)
                .NotEmpty().WithMessage("Жалобы не должны быть пустыми.")
                .MaximumLength(500).WithMessage("Жалобы не должны превышать 500 символов.");

            RuleFor(x => x.Conclusion)
                .NotEmpty().WithMessage("Заключение не должно быть пустым.")
                .MaximumLength(1000).WithMessage("Заключение не должно превышать 1000 символов.");

            RuleFor(x => x.Recommendations)
                .MaximumLength(1000).WithMessage("Рекомендации не должны превышать 1000 символов.");

            RuleFor(x => x.AppointmentId)
                .NotEmpty().WithMessage("Идентификатор встречи не должен быть пустым.");

            RuleFor(x => x.Appointment)
                .NotNull().WithMessage("Информация о встрече должна быть указана.");
        }
    }

}
