using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.DataAccess.Repository
{
    public interface IServiceRepository
    {
        Task<Result<ServiceAppointment>> Create(ServiceAppointment service);
        Task<Result> Delete(Guid id);
        Task<Result<ServiceAppointment>> Update(ServiceAppointment service);
    }
}