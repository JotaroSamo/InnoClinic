using System.ComponentModel.DataAnnotations;

namespace Document_API.Contract.Request.Update
{
    public record PhotoUpdateRequest
    {
        public PhotoUpdateRequest(Guid id, string url)
        {
            Id = id;
            Url = url;
        }

        [Required]
        public Guid Id { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
      
    }
}
