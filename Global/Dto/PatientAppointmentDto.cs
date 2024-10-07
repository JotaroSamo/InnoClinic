using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Dto
{
    public class PatientAppointmentDto
    {
        public Guid Id { get; set; }

        public string Patient_Name { get; set; } = string.Empty;
        public string Number_Phone { get; set; } = string.Empty;
    }
    public class CreatePatient
    {
        public PatientAppointmentDto Patient { get; set; }
    }
    public class UpdatePatient
    {
        public PatientAppointmentDto Patient { get; set; }
    }
    public class DeletePatient
    {
        public Guid Id { get; set; }
    }
}
