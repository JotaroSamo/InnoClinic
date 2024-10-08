using System.ComponentModel.DataAnnotations;

namespace Document_API.Contract.Request.Create
{
    public record PhotoCreateRequest
    {
        public PhotoCreateRequest(string url)
        {
            Url = url;
        }
        [Required]
        [Url]
        public string Url { get; set; }
    }
}
