using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.Domain.Model
{
    public class ServiceCategory
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int TimeSlotSize { get; set; }

        public List<Service> Services { get; set; } = [];
    }
}
