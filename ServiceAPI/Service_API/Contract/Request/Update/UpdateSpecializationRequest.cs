using System.ComponentModel.DataAnnotations;

namespace Service_API.Contract.Request.Update
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
        public Guid Id { get; init; }
        [Required]
        public string SpecializationName { get; init; } = string.Empty;
        [Required]
        public bool IsActive { get; init; }
    }
}
