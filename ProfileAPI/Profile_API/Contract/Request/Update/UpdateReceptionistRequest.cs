using System.ComponentModel.DataAnnotations;

namespace Profile_API.Contract.Request.Update
{
    public record UpdateReceptionistRequest
    {
        public UpdateReceptionistRequest(Guid id, string firstName, string lastName, string middleName, string officeAddress, string officeRegistryPhoneNumber, Guid accountId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            OfficeAddress = officeAddress;
            OfficeRegistryPhoneNumber = officeRegistryPhoneNumber;
            AccountId = accountId;
        }

        [Required]
        public Guid Id { get; set; }
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
