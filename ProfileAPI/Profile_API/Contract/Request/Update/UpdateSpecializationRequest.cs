using System.ComponentModel.DataAnnotations;

namespace Profile_API.Contract.Request.Update
{
    public record UpdateSpecializationRequest
    {
        public UpdateSpecializationRequest(Guid id, string specializationName, bool isActive)
        {
            Id = id;
            SpecializationName = specializationName;
            IsActive = isActive;
        }

        [Required]
        public Guid Id { get; set; }
        [Required]
        public string SpecializationName { get; set; } = string.Empty;
        [Required]
        public bool IsActive { get; set; }
    }
}
