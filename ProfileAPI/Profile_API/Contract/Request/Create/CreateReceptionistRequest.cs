using Profile_API.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Profile_API.Contract.Request.Create
{
    public record CreateReceptionistRequest
    {
        public CreateReceptionistRequest(string firstName, string lastName, string middleName, string officeAddress, string officeRegistryPhoneNumber, Guid accountId)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            OfficeAddress = officeAddress;
            OfficeRegistryPhoneNumber = officeRegistryPhoneNumber;
            AccountId = accountId;
        }

        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string MiddleName { get; set; } = string.Empty;
        public string OfficeAddress { get; set; } = string.Empty;
        [Phone]
        public string OfficeRegistryPhoneNumber { get; set; } = string.Empty;
        [Required]
        public Guid AccountId { get; set; }
    }
}
