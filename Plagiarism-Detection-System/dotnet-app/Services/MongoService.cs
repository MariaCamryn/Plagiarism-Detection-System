using MongoDB.Driver;
using PlagiarismChecker.Web.Models;

namespace PlagiarismChecker.Web.Services
{
    public class MongoService
    {
        private readonly IMongoDatabase _db;
        public MongoService(IConfiguration config)
        {
            var cs = config["MongoSettings:ConnectionString"];
            var dbName = config["MongoSettings:DatabaseName"];
            var client = new MongoClient(cs);
            _db = client.GetDatabase(dbName);
        }

        public IMongoCollection<Submission> Submissions =>
            _db.GetCollection<Submission>(
                // collection name read from config if you want
                Environment.GetEnvironmentVariable("SUBMISSIONS_COLLECTION")
                ?? "submissions");
    }
}
