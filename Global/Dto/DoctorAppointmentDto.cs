using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Dto
{
    public class DoctorAppointmentDto
    {
        public Guid Id { get; set; }
        public string Doctro_Name { get; set; } = string.Empty;
        public string Specialization_Name { get; set; } = string.Empty;

    }
    public class CreateDoctor
    {
        public DoctorAppointmentDto Doctor { get; set; }
    }
    public class UpdateDoctor
    {
        public DoctorAppointmentDto Doctor { get; set; }
    }
    public class DeleteDoctor
    {
        public Guid Id { get; set; }
    }

}
