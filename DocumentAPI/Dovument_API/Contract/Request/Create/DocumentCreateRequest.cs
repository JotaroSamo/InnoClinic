using System.ComponentModel.DataAnnotations;

namespace Document_API.Contract.Request.Create
{
    public record DocumentCreateRequest
    {
        public DocumentCreateRequest(string url, Guid resultId, string complaints, string conclusion, string recommendations)
        {
            Url = url;
            ResultId = resultId;
            Complaints = complaints;
            Conclusion = conclusion;
            Recommendations = recommendations;
        }

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
