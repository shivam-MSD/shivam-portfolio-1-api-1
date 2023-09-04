using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Service;
using System.ComponentModel.DataAnnotations;

namespace ShivamPortfolioWebApisOne.Dtos
{
    public class HomeDto : IDtoWithFilePath
    {
        string _name;
        [Required(ErrorMessage = "Name is required")]
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value?.ToString()?.Trim() ?? null;
            }
        }

        [Required(ErrorMessage = "Description is required")]
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
        public int ImagePriority { get; set; }
        public string ImagePath { get; set; }
    }
}
//public ImagePriorityDto PriorityImage { get; set; }
