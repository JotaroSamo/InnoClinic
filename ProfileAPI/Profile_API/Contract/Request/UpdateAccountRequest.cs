using Profile_API.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Profile_API.Contract.Request
{
    public record UpdateAccountRequest
    {
        [Required]
        public Guid Id { get; set; }
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

        public UpdateAccountRequest(Guid id,Guid userId, string email, string password, Role role, string phoneNumber)
        {
            Id = id;
            UserId = userId;
            Email = email;
            Password = password;
            Role = role;
            PhoneNumber = phoneNumber;
        }
    }
}
