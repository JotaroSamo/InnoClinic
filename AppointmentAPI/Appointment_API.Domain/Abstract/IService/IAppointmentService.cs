using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.Application.Service
{
    public interface IAppointmentService
    {
        Task<Result<Appointment>> ChangeApprovalStatus(Guid id, bool isApproved);
        Task<Result<Appointment>> CreateAppointment(Appointment appointment);
        Task<Result> Delete(Guid id);
        Task<Result<List<Appointment>>> GetAllAppointments();
        Task<Result<Appointment>> GetByIdAppointment(Guid id);
        Task<Result<Appointment>> UpdateAppointment(Appointment appointment);
    }
}