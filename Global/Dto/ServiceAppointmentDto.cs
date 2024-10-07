using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Dto
{
    public class ServiceAppointmentDto
    {
        public Guid Id { get; set; }
        public string Service_Name { get; set; } = string.Empty;

        public decimal Service_Price { get; set; }
    }

    public class CreateService
    {
        public ServiceAppointmentDto Service { get; set; }
    }
    public class UpdateService
    {
        public ServiceAppointmentDto Service { get; set; }
    }
    public class DeleteService
    {
        public Guid Id { get; set; }
    }
}
