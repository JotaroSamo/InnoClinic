using System.ComponentModel.DataAnnotations;

namespace Appointment_API.Contract.Request.Update
{
    public record UpdateAppointmentRequest
    {
        public UpdateAppointmentRequest(Guid id, DateOnly date, TimeOnly time, bool isApproved, Guid patientId, Guid doctorId, Guid serviceId)
        {
            Id = id;
            Date = date;
            Time = time;
            IsApproved = isApproved;
            PatientId = patientId;
            DoctorId = doctorId;
            ServiceId = serviceId;
        }

        [Required]
        public Guid Id { get; set; }
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
