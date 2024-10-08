using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_API.DataAccess.Entity
{
    public class DocumentEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("result_id")]
        public Guid ResultId { get; set; }

        [BsonElement("complaints")]
        public string Complaints { get; set; }

        [BsonElement("conclusion")]
        public string Conclusion { get; set; }

        [BsonElement("recommendations")]
        public string Recommendations { get; set; }
    }
}
