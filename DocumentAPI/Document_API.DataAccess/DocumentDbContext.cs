using Document_API.DataAccess.Entity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document_API.DataAccess
{
    public class DocumentDbContext
    {
        private readonly IMongoDatabase _database;

        public DocumentDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }
        public IMongoCollection<DocumentEntity> Documents => _database.GetCollection<DocumentEntity>("Documents");
        public IMongoCollection<PhotoEntity> Photos => _database.GetCollection<PhotoEntity>("Photos");
    }
}
