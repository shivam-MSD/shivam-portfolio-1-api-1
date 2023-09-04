using ShivamPortfolioWebApisOne.Service;
using System.ComponentModel.DataAnnotations;

namespace ShivamPortfolioWebApisOne.Dtos
{
    public class ProjectsDto : IDtoWithFilePath
    {
        string _projectName;
        [Required(ErrorMessage = "ProjectName is required")]
        public string ProjectName
        {
            get
            {
                return _projectName;
            }

            set
            {
                _projectName = value?.ToString()?.Trim() ?? null;
            }
        }

        string _description;
        [Required(ErrorMessage = "Project Description is required")]
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
        [Required(ErrorMessage = "Project priority is required")]
        public int ProjectPriority { get; set; }

        List<string> _technology;
        public List<string> TechnologyUsed 
        { get
            {
                return _technology;
            }
            set 
            {
                bool isEmptyList = value == null || value.All(string.IsNullOrWhiteSpace);

                if(isEmptyList && value != null)
                {
                    _technology = null;
                }
                else
                {
                    _technology = value.Where(tech => !string.IsNullOrWhiteSpace(tech)).Select(tech => tech.Trim()).ToList() ?? null;
                }
            } 
        }

        string _database;
        public string Database 
        {
            get
            {
                return _database;
            }

            set
            {
                _database = value?.ToString()?.Trim() ?? null;
            }
        }
        public int ImagePriority { get; set; }
        public string ImagePath { get; set; }
    }
}
