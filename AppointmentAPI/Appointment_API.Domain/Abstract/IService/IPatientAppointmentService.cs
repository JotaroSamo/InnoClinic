using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.DataAccess.IService
{
    public interface IPatientAppointmentService
    {
        Task<Result<PatientAppointment>> CreatePatient(PatientAppointment patient);
        Task<Result> DeletePatient(Guid id);
        Task<Result<PatientAppointment>> UpdatePatient(PatientAppointment patient);
    }
}