using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ShivamPortfolioWebApisOne.Model
{
    public class HomeModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, byte[]> PriorityImageDictionary { get; set; }

        public HomeModel() { }
        public HomeModel(string name, string description, Dictionary<string, byte[]> priorityImageDictionary)
        {
            Name = name;
            Description = description;
            if (priorityImageDictionary != null)
            {
                this.PriorityImageDictionary = this.PriorityImageDictionary;
            }
            else
            {
                this.PriorityImageDictionary = new Dictionary<string, byte[]>();
            }
        }
    }
}
