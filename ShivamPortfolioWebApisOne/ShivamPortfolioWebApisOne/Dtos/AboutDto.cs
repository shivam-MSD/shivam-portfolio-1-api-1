using ShivamPortfolioWebApisOne.Service;
using System.ComponentModel.DataAnnotations;

namespace ShivamPortfolioWebApisOne.Dtos
{
    public class AboutDto : IDtoWithFilePath
    {
        string _name;
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

        string _aboutDesc;
        public string AboutDescription 
        {
            get
            {
                return _aboutDesc;
            }

            set
            {
                _aboutDesc = value?.ToString()?.Trim() ?? null;
            }
        }

        string _aboutCareerDesc;
        public string AboutCareerDescription 
        {
            get
            {
                return _aboutCareerDesc;
            }

            set
            {
                _aboutCareerDesc = value?.ToString()?.Trim() ?? null;
            }
        }

        List<string> _hobbies;
        public List<string> Hobbies
        {
            get
            {
                return _hobbies;
            }
            set
            {
                bool isEmptyList = value == null || value.All(string.IsNullOrWhiteSpace);

                if (isEmptyList && value != null)
                {
                    _hobbies = null; // Set _hobbies to null if the input list is empty or contains only empty or white space strings
                }
                else
                {
                    // Filter out the empty or white space strings and trim the non-empty strings
                    _hobbies = value.Where(hobby => !string.IsNullOrWhiteSpace(hobby)).Select(hobby => hobby.Trim()).ToList() ?? null;
                }
            }
        }

        Dictionary<string, string> _skills;
        public Dictionary<string, string> Skills 
        {
            get
            {
                return _skills;
            }
            set
            {
                _skills = value.Where(entry => !string.IsNullOrWhiteSpace(entry.Key) && !string.IsNullOrWhiteSpace(entry.Value))
                .ToDictionary(entry => entry.Key.Trim(), entry => entry.Value.Trim());

                if(_skills.Count == 0)
                {
                    _skills = null;
                }
            }
        }
        public int ImagePriority { get; set; }
        public string ImagePath { get; set; }
    }
}
