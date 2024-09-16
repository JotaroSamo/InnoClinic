using Office_API.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_API.DataAccess.Entity
{
    public class OfficeEntity
    {
        public Guid Id { get; set; }

        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        public string HouseNumber { get; set; } = string.Empty;
        [Required]
        public string OfficeNumber { get; set; } = string.Empty;

        public Guid? PhotoId { get; set; }
        public string? PhotoUrl { get; set; } = string.Empty;
        [Required]
        public string RegistryPhoneNumber { get; set; } = string.Empty;
        [Required]
        public Status IsActive { get; set; }
    }
}
