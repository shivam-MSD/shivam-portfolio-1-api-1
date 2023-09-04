using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ShivamPortfolioWebApisOne.Model
{
    public class AboutModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        //[SwaggerSchema("Description of the item.", ReadOnly = false, Description = "Description of the item.", Type = "string", Format = "multiline")]
        public string AboutCareerDescription { get; set; }
        [DataType(DataType.MultilineText)]
        public string AboutDescription { get; set; }
        public List<string> Hobbies { get; set; }
        public Dictionary<string, string> Skills { get; set; }
        public Dictionary<string, byte[]> PriorityImageDictionary { get; set; }

        public AboutModel()
        {
        }
    }
}
