using System.ComponentModel.DataAnnotations;

namespace Service_API.Contract.Request.Create
{
    public record CreateServiceCategoryRequest
    {
        public CreateServiceCategoryRequest(string categoryName, int timeSlotSize)
        {
            CategoryName = categoryName;
            TimeSlotSize = timeSlotSize;
        }
        [Required]
        public string CategoryName { get; init; } = string.Empty;
        [Required]
        public int TimeSlotSize { get; init; }
    }
}
