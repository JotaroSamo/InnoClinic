using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile_API.DataAccess.Entity
{
   public class ReceptionistEntity
    {

        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string MiddleName { get; set; } = string.Empty;

        public Guid OfficeId { get; set; }
        public string OfficeAddress { get; set; } = string.Empty;
        public string OfficeRegistryPhoneNumber { get; set; } = string.Empty;
        public Guid AccountId { get; set; }
        public AccountEntity Account { get; set; }
    }
}
