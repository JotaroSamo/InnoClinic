
using System.ComponentModel.DataAnnotations;

namespace Appointment_API.Contract.Request.Create
{
    public record CreateAppointmentRequest
    {


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
