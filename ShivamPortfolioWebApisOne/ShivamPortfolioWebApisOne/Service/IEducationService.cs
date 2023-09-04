using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service
{
    public interface IEducationService
    {
        public Task<string> AddEducationData(EducationDto educationDto);
        public Task<string> DeleteEducationData(string id);
        public Task<string> UpdateEducationData(EducationDto educationDto, string id);
        public Task<string> DeleteParticularImage(string id, int priority);
        public Task<List<EducationModel>> GetAllEducationData();
        public EducationModel GetEducationData(string id);
        public Task<string> AddCareerDescription(string id, string careerDescription);
        public Task<string> DeleteCareerDescription(string id);
    }
}
