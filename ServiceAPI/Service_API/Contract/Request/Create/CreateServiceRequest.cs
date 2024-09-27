using System.ComponentModel.DataAnnotations;

namespace Service_API.Contract.Request.Create
{
    public record CreateServiceRequest
    {
        public CreateServiceRequest(Guid categoryId, string serviceName, float price, Guid specializationId, bool isActive)
        {
            CategoryId = categoryId;
            ServiceName = serviceName;
            Price = price;
            SpecializationId = specializationId;
            IsActive = isActive;
        }
        public Guid CategoryId { get; init; }
        [Required]
        public string ServiceName { get; init; } = string.Empty;
        [Required]
        public float Price { get; init; }
        public Guid SpecializationId { get; init; }
        [Required]
        public bool IsActive { get; init; }
    }
}
