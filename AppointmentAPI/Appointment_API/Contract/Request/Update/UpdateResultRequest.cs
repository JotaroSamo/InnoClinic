using Appointment_API.DataAccess.Entity;
using System.ComponentModel.DataAnnotations;

namespace Appointment_API.Contract.Request.Update
{
    public record UpdateResultRequest
    {
        public UpdateResultRequest(Guid id, string complaints, string conclusion, string recommendations, Guid appointmentId)
        {
            Id = id;
            Complaints = complaints;
            Conclusion = conclusion;
            Recommendations = recommendations;
            AppointmentId = appointmentId;
        }

        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Complaints { get; set; } = string.Empty;
        [Required]
        public string Conclusion { get; set; } = string.Empty;
        [Required]
        public string Recommendations { get; set; } = string.Empty;
        [Required]
        public Guid AppointmentId { get; set; }

    }
}
