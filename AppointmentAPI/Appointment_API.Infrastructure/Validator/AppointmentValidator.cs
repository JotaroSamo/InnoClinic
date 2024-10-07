using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.Infrastructure.Validator
{
    using Appointment_API.Domain.Model;
    using FluentValidation;

    public class AppointmentValidator : AbstractValidator<Appointment>
    {
        public AppointmentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Идентификатор встречи не должен быть пустым.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Дата встречи не должна быть пустой.")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now.Date))
                .WithMessage("Дата встречи не может быть в прошлом.");

            RuleFor(x => x.Time)
                .NotEmpty().WithMessage("Время встречи не должно быть пустым.");

            RuleFor(x => x.PatientId)
                .NotEmpty().WithMessage("Идентификатор пациента не должен быть пустым.");

            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("Идентификатор доктора не должен быть пустым.");

            RuleFor(x => x.ServiceId)
                .NotEmpty().WithMessage("Идентификатор услуги не должен быть пустым.");

            RuleFor(x => x.Patient)
                .NotNull().WithMessage("Информация о пациенте должна быть указана.");

            RuleFor(x => x.Doctor)
                .NotNull().WithMessage("Информация о докторе должна быть указана.");

            RuleFor(x => x.Service)
                .NotNull().WithMessage("Информация об услуге должна быть указана.");
        }
    }

}
