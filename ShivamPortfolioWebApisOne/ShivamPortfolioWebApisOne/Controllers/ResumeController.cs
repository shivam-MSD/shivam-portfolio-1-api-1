using Microsoft.AspNetCore.Mvc;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Service;
using ShivamPortfolioWebApisOne.Service.Implementation;

namespace ShivamPortfolioWebApisOne.Controllers
{
    [Route("resume")]
    public class ResumeController : ControllerBase
    {
        private IResumeService resumeService = new ResumeServiceImpl();

        [HttpPost]
        [Route("add-resume")]
        public Task<string> AddResume(ResumeDto resumeDto)
        {
            return resumeService.AddNewResume(resumeDto);
        }

        [HttpPut]
        [Route("update-resume")]
        public Task<string> UpdateResume(ResumeDto resumeDto,string id)
        {
            return resumeService.UpdateResume(resumeDto,id);
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<ResumeModel>> GetAllResume()
        {
            return await resumeService.GetAllResume();
        }

        [HttpDelete]
        [Route("delete-resume")]
        public Task<string> DeleteResume(string id)
        {
            return resumeService.DeleteResume(id);
        }

        [HttpGet]
        [Route("get")]
        public ResumeModel GetParticularResume(string id)
        {
            return resumeService.GetResume(id);
        }


        [HttpGet]
        [Route("get/1")]
        public ResumeModel GetFirstResume()
        {
            return resumeService.GetAllResume().Result.FirstOrDefault();
        }
    }
}
