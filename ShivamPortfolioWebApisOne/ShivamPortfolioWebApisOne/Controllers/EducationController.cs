using Microsoft.AspNetCore.Mvc;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Service;
using ShivamPortfolioWebApisOne.Service.Implementation;

namespace ShivamPortfolioWebApisOne.Controllers
{
    [Route("api/education")]
    public class EducationController : ControllerBase
    {
        private IEducationService educationService = new EducationServiceImpl();
        [HttpPost]
        [Route("adddata")]
        public Task<string> AddEducationDetails(EducationDto educationDto)
        {
            return educationService.AddEducationData(educationDto);
        }

        [HttpPut]
        [Route("update")]
        public Task<string> UpdateEducationDetails(EducationDto educationDto, string id)
        {
            return educationService.UpdateEducationData(educationDto, id);
        }

        [HttpDelete]
        [Route("deleteeducation")]
        public Task<string> DeleteEducation(string id)
        {
            return educationService.DeleteEducationData(id);
        }

        [HttpDelete]
        [Route("delete-image")]
        public Task<string> DeleteParticularImage(string id, int priority)
        {
            return educationService.DeleteParticularImage(id, priority);
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<EducationModel>> GetAllEducation()
        {
            return await educationService.GetAllEducationData();
        }

        [HttpGet]
        [Route("")]
        public EducationModel GetEducationDetail(string id)
        {
            return educationService.GetEducationData(id);
        }

        [HttpPut]
        [Route("addcareerdescription")]
        public Task<string> AddCareerDescription(string id, string careerDeescription)
        {
            return educationService.AddCareerDescription(id, careerDeescription);
        }

        [HttpPut]
        [Route("removecareerdescription")]
        public Task<string> RemoveCareerDescription(string id)
        {
            return educationService.DeleteCareerDescription(id);
        }
    }
}
