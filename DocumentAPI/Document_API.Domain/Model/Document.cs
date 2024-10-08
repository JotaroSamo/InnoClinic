using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_API.Domain.Model
{
    public class Document
    {
       
        public Guid Id { get; set; }

  
        public string Url { get; set; } = string.Empty;

       
        public Guid ResultId { get; set; }

 
        public string Complaints { get; set; } = string.Empty;


        public string Conclusion { get; set; } = string.Empty;

       
        public string Recommendations { get; set; } = string.Empty;
    }
}
