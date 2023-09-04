using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ShivamPortfolioWebApisOne.Model
{
    public class ProjectsModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ProjectName { get; set; }
        public int ProjectPriority { get; set; }
        public string Description { get; set; }
        public List<string> TechnologyUsed { get; set; }
        public string Database { get; set; }
        public Dictionary<string, byte[]> PriorityImageDictionary { get; set; }

    }
}
