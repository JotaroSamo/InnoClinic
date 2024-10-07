using Appointment_API.Domain.Model;
using CSharpFunctionalExtensions;

namespace Appointment_API.DataAccess.IService
{
    public interface IDoctorAppointmentService
    {
        Task<Result<DoctorAppointment>> CreateDoctor(DoctorAppointment doctor);
        Task<Result> DeleteDoctor(Guid id);
        Task<Result<DoctorAppointment>> UpdateDoctor(DoctorAppointment doctor);
    }
}