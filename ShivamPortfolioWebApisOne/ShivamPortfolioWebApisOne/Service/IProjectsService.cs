using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service
{
    public interface IProjectsService
    {
        public Task<string> AddProject(ProjectsDto projectsDto);

        public Task<string> UpdateProject(ProjectsDto projectsDto, string id);

        public Task<string> DeleteParticularImage(string id, int priority);

        public Task<string> DeleteProject(string id);

        public Task<List<ProjectsModel>> GetAllProjects();

        public ProjectsModel GetProject(string id);

    }
}
