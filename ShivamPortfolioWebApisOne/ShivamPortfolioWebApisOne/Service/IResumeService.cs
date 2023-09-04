using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service
{
    public interface IResumeService
    {
        public Task<string> AddNewResume(ResumeDto resumeDto);

        public Task<string> DeleteResume(string resumeId);

        public Task<string> UpdateResume(ResumeDto resumeDto,string id);

        public ResumeModel GetResume(string id);

        public Task<List<ResumeModel>> GetAllResume();
    }
}
