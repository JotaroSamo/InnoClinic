using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.DataAccess.Entity
{
    public class AccountEntity
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public bool IsEmailVerified { get; set; }


        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
        public string PhotoId { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;

        public ReceptionistEntity? Receptionist { get; set; }
        public DoctorEntity? Doctor { get; set; }
        public PatientEntity? Patient { get; set; }
    }
}
