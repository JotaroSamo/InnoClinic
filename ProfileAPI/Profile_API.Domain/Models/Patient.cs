using System.ComponentModel.DataAnnotations;

namespace Profile_API.Domain.Models;

public class Patient
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string MiddleName { get; set; } = string.Empty;
    public bool IsLinkedToAccount { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int AccountId { get; set; }
    public Account Account { get; set; }
}