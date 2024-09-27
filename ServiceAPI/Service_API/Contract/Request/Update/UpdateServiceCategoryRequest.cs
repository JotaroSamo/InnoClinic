using System.ComponentModel.DataAnnotations;

namespace Service_API.Contract.Request.Update
{
    public record UpdateServiceCategoryRequest
    {
        public UpdateServiceCategoryRequest(Guid id, string categoryName, int timeSlotSize)
        {
            Id = id;
            CategoryName = categoryName;
            TimeSlotSize = timeSlotSize;
        }
        [Required]
        public Guid Id { get; init; }
        [Required]
        public string CategoryName { get; init; } = string.Empty;
        [Required]
        public int TimeSlotSize { get; init; }
    }
}
