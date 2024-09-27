using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Domain.Model
{
    public class Service
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public float Price { get; set; }
        public Guid SpecializationId { get; set; }
        public bool IsActive { get; set; }

        public ServiceCategory ServiceCategory { get; set; }
        public Specialization Specialization { get; set; }
    }
}
