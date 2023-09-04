using Microsoft.AspNetCore.Mvc;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Service;
using ShivamPortfolioWebApisOne.Service.Implementation;

namespace ShivamPortfolioWebApisOne.Controllers
{
    [Route("api/workexperience")]
    public class WorkExperienceController : ControllerBase
    {
        private IWorkExperienceService workExperienceService = new WorkExperienceServiceImpl();

        [HttpPost]
        [Route("adddata")]
        public Task<string> AddWorkExperienceData(WorkExperienceDto workExperienceDto)
        {
            return workExperienceService.AddWorkExperience(workExperienceDto);
        }

        [HttpPut]
        [Route("update")]
        public Task<string> UpdateWorkExperience(WorkExperienceDto workExperienceDto,string id) 
        {
            return workExperienceService.UpdateWorkExperience(workExperienceDto,id);
        }

        [HttpDelete]
        [Route("delete-image")]
        public Task<string> DeleteParticularImage(string id,int priority)
        {
            return workExperienceService.DeleteParticularImage(id,priority);
        }

        [HttpDelete]
        [Route("delete")]
        public Task<string> DeleteWorkExperience(string id)
        {
            return workExperienceService.DeleteWorkExperience(id);
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<WorkExperienceModel>> GetAllWorkExperience()
        {
            return await workExperienceService.GetAllWorkExperience();
        }

        [HttpDelete]
        [Route("remove-logo")]
        public Task<string> RemoveCompanyLogo(string id)
        {
            return workExperienceService.RemoveLogo(id);
        }
    }
}
