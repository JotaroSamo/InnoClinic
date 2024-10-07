using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Entity
{
    public class DoctorAppointmentEntity
    {
        [Key]
        public Guid Guid { get; set; }
        [Required]
        public string Doctro_Name { get; set; } = string.Empty;
        [Required]
        public string Specialization_Name { get; set; } = string.Empty;

        public ICollection<AppointmentEntity>? Appointments { get; set; }
    }
}
