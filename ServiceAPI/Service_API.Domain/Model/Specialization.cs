using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Domain.Model
{
    public class Specialization
    {
        public Guid Id { get; set; }
        public string SpecializationName { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public List<Service> Services { get; set; } = new();

    }
}
