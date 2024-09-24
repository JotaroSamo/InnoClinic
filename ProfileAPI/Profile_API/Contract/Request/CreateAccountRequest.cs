using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Profile_API.Domain.Enums;

namespace Profile_API.Contract.Request
{
    public record CreateAccountRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public CreateAccountRequest(Guid userId, string email, string password, Role role, string phoneNumber)
        {
            UserId = userId;
            Email = email;
            Password = password;
            Role = role;
            PhoneNumber = phoneNumber;
        }
    }
}
