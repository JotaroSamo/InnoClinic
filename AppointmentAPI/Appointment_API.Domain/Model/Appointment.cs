using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.Domain.Model
{
    public class Appointment
    {
       public Guid Id { get; set; }
       public DateOnly Date { get; set; }
       public TimeOnly Time { get; set; }
       public bool IsApproved { get; set; }
       
        public Guid PatientId { get; set; }
        public PatientAppointment Patient { get; set; }

        public Guid DoctorId { get; set; }
        public DoctorAppointment Doctor { get; set; }

        public Guid ServiceId { get; set; }
        public ServiceAppointment Service { get; set; }

        


    }
}
