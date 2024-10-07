using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.Application.Service
{
    public interface IPatientAppointmentService
    {
        Task<Result<PatientAppointment>> CreatePatient(PatientAppointment patient);
        Task<Result> DeletePatient(Guid id);
        Task<Result<List<PatientAppointment>>> GetAllPatients();
        Task<Result<PatientAppointment>> GetPatientById(Guid id);
        Task<Result<PatientAppointment>> UpdatePatient(PatientAppointment patient);
    }
}