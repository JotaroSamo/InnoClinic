using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Auth_API.Contract
{
    public record UserRegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; init; } = string.Empty;

        [Required]
        [MaxLength(15)]
        [MinLength(6)]
        [PasswordPropertyText]
        public string Password { get; init; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; init; } = string.Empty;
    }
}
