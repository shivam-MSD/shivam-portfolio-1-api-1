using ShivamPortfolioWebApisOne.Service;
using System.ComponentModel.DataAnnotations;

namespace ShivamPortfolioWebApisOne.Dtos
{
    public class HeaderDto : IDtoWithFilePath
    {
        [Required(ErrorMessage = "ImagePriority of image logo is required")]
        public int ImagePriority { get; set; }
        public string ImagePath { get; set; }
    }
}
