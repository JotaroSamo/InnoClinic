using System.ComponentModel.DataAnnotations;

namespace Service_API.Contract.Request.Update
{
    public record UpdateServiceRequest
    {
        public UpdateServiceRequest(Guid id, Guid categoryId, string serviceName, float price, Guid specializationId, bool isActive)
        {
            Id = id;
            CategoryId = categoryId;
            ServiceName = serviceName;
            Price = price;
            SpecializationId = specializationId;
            IsActive = isActive;
        }

        [Required]
        public Guid Id { get; init; }
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
