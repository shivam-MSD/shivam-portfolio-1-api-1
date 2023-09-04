using Microsoft.AspNetCore.Mvc;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Service;
using ShivamPortfolioWebApisOne.Service.Implementation;

namespace ShivamPortfolioWebApisOne.Controllers
{
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private IProjectsService projectsService = new ProjectServiceImpl();

        [HttpPost]
        [Route("addproject")]
        public Task<string> AddProjects(ProjectsDto projectsDto)
        {
            return projectsService.AddProject(projectsDto);
        }

        [HttpPut]
        [Route("update")]
        public Task<string> UpdateProject(ProjectsDto projectsDto,string id) 
        {
            return projectsService.UpdateProject(projectsDto,id);
        }

        [HttpDelete]
        [Route("delete")]
        public Task<string> DeleteProject(string id)
        {
            return projectsService.DeleteProject(id);
        }

        [HttpDelete]
        [Route("delete-image")]
        public Task<string> DeleteParticularImage(string id,int priority)
        {
            return projectsService.DeleteParticularImage(id,priority);
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<ProjectsModel>> GetAllProjects() 
        {
            return await projectsService.GetAllProjects();
        }

        [HttpGet]
        [Route("")]
        public ProjectsModel GetProject(string id)
        {
            return projectsService.GetProject(id);
        }
    }
}
