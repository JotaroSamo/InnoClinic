using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Auth_API.Contract
{
    public record UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required]
        [MaxLength(15)]
        [MinLength(6)]
        [PasswordPropertyText]
        public string Password { get; init; } = string.Empty;
    }
}
