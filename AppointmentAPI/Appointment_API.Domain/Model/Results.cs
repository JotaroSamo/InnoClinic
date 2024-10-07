using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.Domain.Model
{
    public class Results
    {
        public Guid Id { get; set; }
        public string Complaints { get; set; } = string.Empty;
        public string Conclusion { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;

        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
