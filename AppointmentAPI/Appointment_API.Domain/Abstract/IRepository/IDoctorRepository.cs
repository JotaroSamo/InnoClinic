using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.DataAccess.Repository
{
    public interface IDoctorRepository
    {
        Task<Result<DoctorAppointment>> Create(DoctorAppointment doctor);
        Task<Result> Delete(Guid id);
        Task<Result<DoctorAppointment>> Update(DoctorAppointment doctor);
    }
}