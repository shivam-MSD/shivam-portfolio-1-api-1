using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Service;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShivamPortfolioWebApisOne.Dtos
{
    public class EducationDto : IDtoWithFilePath
    {
        string _schoolName;
        [Required(ErrorMessage = "School Name is required")]
        public string SchoolName
        {
            get
            {
                return _schoolName;
            }

            set
            {
                _schoolName = value?.ToString()?.Trim() ?? null;
            }
        }

        string _educationBoard;
        [Required(ErrorMessage = "Education Board is required")]
        public string EducationBoard 
        {
            get
            {
                return _educationBoard;
            }

            set
            {
                _educationBoard = value?.ToString()?.Trim() ?? null;
            }
        }

        [Required(ErrorMessage = "School or College is required")]
        public SchoolType TypeOfSchool { get; set; }

        string _description;
        public string Description 
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value?.ToString()?.Trim() ?? null;
            }
        }

        string _careerDescription;
        public string MyCareerDescription 
        {
            get
            {
                return _careerDescription;
            }

            set
            {
                _careerDescription = value?.ToString()?.Trim() ?? null;
            }
        }


        public int ImagePriority { get; set; }
        public string ImagePath { get; set; }

        string _fieldname;
        public string FieldName 
        {
            get
            {
                return _fieldname;
            }

            set
            {
                _fieldname = value?.ToString()?.Trim() ?? null;
            }
        }
        public float Grades { get; set; }
    }
}
