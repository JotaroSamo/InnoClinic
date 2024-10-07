using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.DataAccess.Repository
{
    public interface IPatientRepository
    {
        Task<Result<PatientAppointment>> Create(PatientAppointment patient);
        Task<Result> Delete(Guid id);
        Task<Result<List<PatientAppointment>>> GetAll();
        Task<Result<PatientAppointment>> GetById(Guid id);
        Task<Result<PatientAppointment>> Update(PatientAppointment patient);
    }
}