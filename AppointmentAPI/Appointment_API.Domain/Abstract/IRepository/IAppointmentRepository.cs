using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.DataAccess.Repository
{
    public interface IAppointmentRepository
    {
        Task<Result<Appointment>> ChangeApprovalStatus(Guid id, bool isApproved);
        Task<Result<Appointment>> Create(Appointment appointment);
        Task<Result> Delete(Guid id);
        Task<Result<List<Appointment>>> GetAll();
        Task<Result<Appointment>> GetById(Guid id);
        Task<Result<Appointment>> Update(Appointment appointment);
    }
}