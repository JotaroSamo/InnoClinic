using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_API.Domain.Model
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}
