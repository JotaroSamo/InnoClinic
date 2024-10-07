using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Entity
{
   public class AppointmentEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeOnly Time { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        [Required]
        public Guid PatientId { get; set; }
        public PatientAppointmentEntity Patient { get; set; }
        [Required]
        public Guid DoctorId { get; set; }
        public DoctorAppointmentEntity Doctor { get; set; }
        [Required]
        public Guid ServiceId { get; set; }
        public ServiceAppointmentEntity Service { get; set; }
    }
}
