using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.DataAccess.Entity
{
    public class SpecializationEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string SpecializationName { get; set; } = string.Empty;
        [Required]
        public bool IsActive { get; set; }

        public List<ServiceEntity> Services { get; set; } = [];
    }
}
