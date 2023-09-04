using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ShivamPortfolioWebApisOne.Model
{
    public class Data
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; }

        public string Name { get; set; }
        public string Description { get; set; }

        public Data(string Name,string Description)
        {
            this.Name = Name;
            this.Description = Description;
        }
    }
}
