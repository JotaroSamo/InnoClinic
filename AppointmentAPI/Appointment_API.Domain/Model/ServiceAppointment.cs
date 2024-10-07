using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.Domain.Model
{
    public class ServiceAppointment
    {
        public Guid Id { get; set; }
        public string Service_Name { get; set; } = string.Empty;

        public decimal Service_Price { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
