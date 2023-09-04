using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ShivamPortfolioWebApisOne.Model
{
    public class ArticlesModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Dictionary<string,string> Link { get; set; }
        public byte[] Image { get; set; }
        public int Priority { get; set; }
    }
}
