using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.Dto
{
    public class SpecializationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
    public class CreateSpecialization
    {
        public SpecializationDto Specialization { get; set; }
    }

    public class UpdateSpecialization
    {
        public SpecializationDto Specialization { get; set; }
    }

    public class DeleteSpecialization
    {
        public Guid Id { get; set; }
    }
}
