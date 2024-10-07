using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.Application.Service
{
    public interface IServiceAppointmentService
    {
        Task<Result<ServiceAppointment>> CreateService(ServiceAppointment service);
        Task<Result> DeleteService(Guid id);
        Task<Result<List<ServiceAppointment>>> GetAllServices();
        Task<Result<ServiceAppointment>> GetServiceById(Guid id);
        Task<Result<ServiceAppointment>> UpdateService(ServiceAppointment service);
    }
}