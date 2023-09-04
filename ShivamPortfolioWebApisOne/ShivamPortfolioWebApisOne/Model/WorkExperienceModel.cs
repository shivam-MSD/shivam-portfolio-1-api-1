using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ShivamPortfolioWebApisOne.Model
{
    public class WorkExperienceModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public Dictionary<string, byte[]> PriorityImageDictionary { get; set; }
        public string PositionName { get; set; }
        public int NoOfMnthOrYearExperience { get; set; }
        public MonthsOrYears MonthOrYearExperience { get; set; }
        public byte[] Logo { get; set; }
        public int priority { get; set; }
        public string CompanyWebSiteLink { get; set; }

        public WorkExperienceModel()
        {
        }

        public WorkExperienceModel(string companyName, string description, Dictionary<string, byte[]> priorityImageDictionary, string positionName, int noOfMnthOrYearExperience, MonthsOrYears monthOrYearExperience, byte[] logo)
        {
            CompanyName = companyName;
            Description = description;
            this.PriorityImageDictionary = priorityImageDictionary;
            PositionName = positionName;
            NoOfMnthOrYearExperience = noOfMnthOrYearExperience;
            MonthOrYearExperience = monthOrYearExperience;
            Logo = logo;
        }
    }

    public enum MonthsOrYears
    {
        Month = 1,
        Year = 2,
    }
}
