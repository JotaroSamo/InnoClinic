using System.ComponentModel.DataAnnotations;

namespace Document_API.Contract.Request.Update
{
    public record DocumentUpdateRequest
    {
        public DocumentUpdateRequest(Guid id, string url, Guid resultId, string complaints, string conclusion, string recommendations)
        {
            Id = id;
            Url = url;
            ResultId = resultId;
            Complaints = complaints;
            Conclusion = conclusion;
            Recommendations = recommendations;
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        [Url]
        public string Url { get; set; } = string.Empty;

        [Required]
        public Guid ResultId { get; set; }

        [Required]
        public string Complaints { get; set; } = string.Empty;

        [Required]
        public string Conclusion { get; set; } = string.Empty;

        [Required]
        public string Recommendations { get; set; } = string.Empty;
    }
}
