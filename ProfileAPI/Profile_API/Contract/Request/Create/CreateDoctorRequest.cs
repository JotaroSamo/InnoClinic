using Profile_API.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Profile_API.Contract.Request.Create
{
    public record CreateDoctorRequest
    {
        public CreateDoctorRequest(string firstName, string lastName, string middleName, DateTime dateOfBirth, DateTime careerStartYear, Status status, Guid specializationId, string officeAddress, string officeRegistryPhoneNumber, Guid accountId)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            DateOfBirth = dateOfBirth;
            CareerStartYear = careerStartYear;
            Status = status;
            SpecializationId = specializationId;
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
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public DateTime CareerStartYear { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public Guid SpecializationId { get; set; }
        public string OfficeAddress { get; set; } = string.Empty;
        [Phone]
        public string OfficeRegistryPhoneNumber { get; set; } = string.Empty;
        [Required]
        public Guid AccountId { get; set; }

    }
}
