using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Entity
{
    public class DoctorAppointmentEntity
    {
        public Guid Guid { get; set; }
        public string Doctro_Name { get; set; } = string.Empty;
        public string Specialization_Name { get; set; } = string.Empty;

        public ICollection<AppointmentEntity>? Appointments { get; set; }
    }
}
