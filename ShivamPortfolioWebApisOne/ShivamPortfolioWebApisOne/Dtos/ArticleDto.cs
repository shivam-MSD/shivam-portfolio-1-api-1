using ShivamPortfolioWebApisOne.Service;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShivamPortfolioWebApisOne.Dtos
{
    public class ArticleDto : IDtoWithFilePath
    {
        string _title;
        [Required(ErrorMessage = "Title is required")]
        public string Title 
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value?.ToString()?.Trim() ?? null;
            }
        }

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

        Dictionary<string, string> _link;
        public Dictionary<string, string> Link 
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value.Where(entry => !string.IsNullOrWhiteSpace(entry.Key) && !string.IsNullOrWhiteSpace(entry.Value))
                .ToDictionary(entry => entry.Key.Trim(), entry => entry.Value.Trim());

                if(_link.Count == 0)
                {
                    _link = null;
                }
            }
        }
        /// <summary>
        /// This priority is used display the article in the specific sequence.
        /// </summary>
        public int ImagePriority { get; set; }
        public string ImagePath { get; set; }
    }
}
