using Office_API.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Office_API.Contract
{
    public record OfficeUpdateRequest
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        public string HouseNumber { get; set; } = string.Empty;
        [Required]
        public string OfficeNumber { get; set; } = string.Empty;
        [Required]
        [Phone]
        public string RegistryPhoneNumber { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        [Required]
        public Status IsActive { get; set; }
    }
}
