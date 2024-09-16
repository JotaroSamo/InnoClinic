using Auth_API.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth_API.DataAccess.Entity
{
    public class UserEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
       
        public string HashPassword { get; set; } = string.Empty;
        [Required]
        public Role Role { get; set; }

        public string RefreshToken { get; set; } = string.Empty;
    }
}
