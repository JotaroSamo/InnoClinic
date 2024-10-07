using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.Application.Service
{
    public interface IDoctorAppointmentService
    {
        Task<Result<DoctorAppointment>> CreateDoctor(DoctorAppointment doctor);
        Task<Result> DeleteDoctor(Guid id);
        Task<Result<List<DoctorAppointment>>> GetAllDoctors();
        Task<Result<DoctorAppointment>> GetDoctorById(Guid id);
        Task<Result<DoctorAppointment>> UpdateDoctor(DoctorAppointment doctor);
    }
}