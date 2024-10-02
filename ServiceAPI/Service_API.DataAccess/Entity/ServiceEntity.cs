using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.DataAccess.Entity
{
    public class ServiceEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        [Required]
        public string ServiceName { get; set; } = string.Empty;
        [Required]
        public float Price { get; set; }
        public Guid SpecializationId { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public ServiceCategoryEntity ServiceCategory { get; set; }
        public SpecializationEntity Specialization { get; set; }
    }
}
