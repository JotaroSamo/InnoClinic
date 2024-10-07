using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Entity
{
    public class ServiceAppointmentEntity
    {
        public Guid Id { get; set; }
        public string Service_Name { get; set; } = string.Empty;

        public float Service_Price { get; set; }

        public ICollection<AppointmentEntity>? Appointments { get; set; }
    }
}
