using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace ShivamPortfolioWebApisOne.Model
{
    public class ContactModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Dictionary<string, string> ContactDetailsLinks { get; set; }
    }
}
