using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.Domain.Model
{
    public class DoctorAppointment
    {
        public Guid Id { get; set; }
        public string Doctro_Name { get; set; } = string.Empty;
        public string Specialization_Name { get; set; } = string.Empty;

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
