using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Entity
{
    public class PatientAppointmentEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Patient_Name { get; set; } = string.Empty;

        [Phone]
        [Required]
        public string Number_Phone { get; set; } = string.Empty;

        [EmailAddress]
        public string Patient_Email { get; set; } = string.Empty;

        public ICollection<AppointmentEntity>? Appointments { get; set; }
    }
}
