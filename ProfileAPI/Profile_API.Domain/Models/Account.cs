using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Profile_API.Domain.Models;

public class Account
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public bool IsEmailVerified { get; set; }
    public string PhotoId { get; set; } = string.Empty;

    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string UpdatedBy { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public string PhotoUrl { get; set; } = string.Empty;

    public Receptionist? Receptionist { get; set; }
    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}