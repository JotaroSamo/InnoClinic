using System.ComponentModel.DataAnnotations;

namespace Service_API.Contract.Request.Create
{
    public record CreateSpecializationRequest
    {
        public CreateSpecializationRequest(string specializationName, bool isActive)
        {
            SpecializationName = specializationName;
            IsActive = isActive;
        }

        [Required]
        public string SpecializationName { get; init; } = string.Empty;
        [Required]
        public bool IsActive { get; init; }
    }
}
