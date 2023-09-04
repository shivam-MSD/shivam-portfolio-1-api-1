using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ShivamPortfolioWebApisOne.Model
{
    public class EducationModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SchoolName { get; set; }
        public string EducationBoard { get; set; }
        public SchoolType TypeOfSchool { get; set; }
        public string Description { get; set; }
        public Dictionary<string, byte[]> PriorityImageDictionary { get; set; }
        public string FieldName { get; set; }
        public float Grades { get; set; }
        public string MyCareerDescription { get; set; }

        public EducationModel()
        {
        }

        public EducationModel(string schoolName, string educationBoard, SchoolType typeOfSchool, string descrption, Dictionary<string, byte[]> priorityImageDictionary, string fieldName, float grades)
        {
            SchoolName = schoolName;
            EducationBoard = educationBoard;
            TypeOfSchool = typeOfSchool;
            Description = descrption;
            this.PriorityImageDictionary = priorityImageDictionary;
            FieldName = fieldName;
            Grades = grades;
        }
    }

    public enum SchoolType
    {
        PrimarySchool = 1,
        SecondarySchool = 2,
        HigherSecondarySchool = 3,
        College = 4,
        University = 5,
    }
}
