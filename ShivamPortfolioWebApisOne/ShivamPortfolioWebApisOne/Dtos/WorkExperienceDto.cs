using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Service;
using System.ComponentModel.DataAnnotations;

namespace ShivamPortfolioWebApisOne.Dtos
{
    public class WorkExperienceDto : IDtoWithFilePath
    {
        string _companyName;
        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName 
        { get
            {
                return _companyName;
            }
            set
            {
                _companyName = value?.ToString()?.Trim() ?? null;
            }
        }
        string _description;
        [Required(ErrorMessage = "Company description is required")]
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
        public int ImagePriority { get; set; }
        public string ImagePath { get; set; }

        string _positionName;
        public string PositionName 
        {
            get
            {
                return _positionName;
            }
            set
            {
                _positionName = value?.ToString()?.Trim() ?? null;
            }
        }
        public int NoOfMnthOrYearExperience { get; set; }
        public MonthsOrYears MonthOrYearExperience { get; set; }
        public string LogoImagePath { get; set; }
        public string CompanyWebSiteLink { get; set; }

        [Required(ErrorMessage = "Company priority is required")]
        public int CompanyPriority { get; set; }
    }
}
