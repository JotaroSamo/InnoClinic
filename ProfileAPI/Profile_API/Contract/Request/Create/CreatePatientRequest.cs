using Profile_API.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Profile_API.Contract.Request.Create
{
    public record CreatePatientRequest
    {
        public CreatePatientRequest(string firstName, string lastName, string middleName, bool isLinkedToAccount, DateTime dateOfBirth, Guid accountId)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            IsLinkedToAccount = isLinkedToAccount;
            DateOfBirth = dateOfBirth;
            AccountId = accountId;
        }

        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string MiddleName { get; set; } = string.Empty;
        [Required]
        public bool IsLinkedToAccount { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Guid AccountId { get; set; }
    }
}
