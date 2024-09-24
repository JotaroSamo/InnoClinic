using System.ComponentModel.DataAnnotations;
using Profile_API.Domain.Models;

namespace Profile_API.DataAccess.Entity;

public class SpecializationEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string SpecializationName { get; set; } = string.Empty;
    [Required]
    public bool IsActive { get; set; }

    public List<DoctorEntity> Doctors { get; set; }
}