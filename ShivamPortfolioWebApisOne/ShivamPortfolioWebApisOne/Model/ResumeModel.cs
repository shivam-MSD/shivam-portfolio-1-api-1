using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ShivamPortfolioWebApisOne.Model
{
    public class ResumeModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public byte[] Resume { get; set; }

        public string ResumeLink { get; set; }
    }
}
