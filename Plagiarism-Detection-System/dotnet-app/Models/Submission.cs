using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PlagiarismChecker.Web.Models
{
    public class Submission
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string Content { get; set; } = string.Empty;
        public double Score { get; set; }
        public string TopMatchesJson { get; set; } = "{}";
        public string HighlightsJson { get; set; } = "[]";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
