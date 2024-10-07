using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment_API.DataAccess.Entity
{
    public class PatientAppointmentEntity
    {
        public Guid Id { get; set; }

        public string Patient_Name { get; set; } = string.Empty;

        [Phone]
        public string Number_Phone { get; set; } = string.Empty;

        public ICollection<AppointmentEntity>? Appointments { get; set; }
    }
}
