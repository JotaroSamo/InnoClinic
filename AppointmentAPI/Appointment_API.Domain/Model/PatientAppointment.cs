using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.Domain.Model
{
    public class PatientAppointment
    {
        public Guid Id { get; set; }

        public string Patient_Name { get; set; } = string.Empty;

        [Phone]
        public string Number_Phone { get; set; } = string.Empty;

        [EmailAddress]
        public string Patient_Email { get; set; } = string.Empty;

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
