
using System.ComponentModel.DataAnnotations;

namespace Appointment_API.Contract.Request.Create
{
    public record CreateAppointmentRequest
    {
        public CreateAppointmentRequest(DateOnly date, TimeOnly time, bool isApproved, Guid patientId, Guid doctorId, Guid serviceId)
        {
            Date = date;
            Time = time;
            IsApproved = isApproved;
            PatientId = patientId;
            DoctorId = doctorId;
            ServiceId = serviceId;
        }

        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeOnly Time { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public Guid ServiceId { get; set; }
      
    }
}
