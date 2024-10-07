using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.DataAccess.IService
{
    public interface IServiceAppointmentService
    {
        Task<Result<ServiceAppointment>> CreateService(ServiceAppointment service);
        Task<Result> DeleteService(Guid id);
        Task<Result<ServiceAppointment>> UpdateService(ServiceAppointment service);
    }
}