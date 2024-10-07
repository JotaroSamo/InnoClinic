
using Profile_API.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.DataAccess.Entity
{
    public class DoctorEntity
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string MiddleName { get; set; } = string.Empty;
        [Required]
        public DateOnly DateOfBirth { get; set; }
        [Required]
        public DateOnly CareerStartYear { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public Guid SpecializationId { get; set; }
        public SpecializationEntity Specialization { get; set; }
        public Guid OfficeId { get; set; }
        public string OfficeAddress { get; set; } = string.Empty;
        public string OfficeRegistryPhoneNumber { get; set; } = string.Empty;

        public Guid AccountId { get; set; }
        public AccountEntity Account { get; set; }
    }
}

