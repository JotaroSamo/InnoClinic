using System.ComponentModel.DataAnnotations;

namespace Profile_API.Domain.Models;

public class Specialization
{
    public Guid Id { get; set; }
    [Required]
    public string SpecializationName { get; set; } = string.Empty;

    [Required]
    public bool IsActive { get; set; }

    public List<Doctor> Doctors { get; set; }
}