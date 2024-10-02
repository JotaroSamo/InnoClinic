using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_API.DataAccess.Entity
{
    public class ServiceCategoryEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        public int TimeSlotSize { get; set; }

        public List<ServiceEntity> Services { get; set; } = [];
    }
}
