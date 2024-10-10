using Appointment_API.Domain.Model;

namespace Appointment_API.Domain.Abstract.IService
{
    public interface IEmailService
    {
        Task SendAppointmentResultOnEmail(Results result);
        Task SendNotificationAboutAppointmentToEmail(Appointment appointment);
    }
}