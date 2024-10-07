using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Entity
{
    public class ResultEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Complaints { get; set; } = string.Empty;
        [Required]
        public string Conclusion { get; set; } = string.Empty;
        [Required]
        public string Recommendations { get; set; } = string.Empty;
        [Required]
        public Guid AppointmentId { get; set; }
        public AppointmentEntity Appointment { get; set; }
    }
}
