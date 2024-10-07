using System.ComponentModel.DataAnnotations;

namespace Appointment_API.Contract.Request.Create
{
    public record CreateResultRequest 
    {
        public CreateResultRequest(Guid appointmentId, string complaints, string conclusion, string recommendations)
        {
            AppointmentId = appointmentId;
            Complaints = complaints;
            Conclusion = conclusion;
            Recommendations = recommendations;
        }

        [Required]
        public Guid AppointmentId { get; set; }

        [Required]
        public string Complaints { get; set; } = string.Empty;
        [Required]
        public string Conclusion { get; set; } = string.Empty;
        [Required]
        public string Recommendations { get; set; } = string.Empty;
    }
}
