using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Entity
{
    public class ServiceAppointmentEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Service_Name { get; set; } = string.Empty;
        [Required]
        public decimal Service_Price { get; set; }

        public ICollection<AppointmentEntity>? Appointments { get; set; }
    }
}
