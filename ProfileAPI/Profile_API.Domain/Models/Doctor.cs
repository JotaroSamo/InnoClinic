using Profile_API.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Profile_API.Domain.Models;

public class Doctor
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string MiddleName { get; set; } = string.Empty;
    [Required]
    public DateTime DateOfBirth { get; set; }
    public Guid CareerStartYear { get; set; }
    [Required]
    public Status Status { get; set; }
    public Guid SpecializationId { get; set; }
    [Required]
    public Specialization Specialization { get; set; }
    public Guid OfficeId { get; set; }
    public string OfficeAddress { get; set; } = string.Empty;
    public string OfficeRegistryPhoneNumber { get; set; } = string.Empty;

    public Guid AccountId { get; set; }
    public Account Account { get; set; }
}