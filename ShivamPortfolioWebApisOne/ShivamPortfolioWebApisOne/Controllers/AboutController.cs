using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Service;
using ShivamPortfolioWebApisOne.Service.Implementation;

namespace ShivamPortfolioWebApisOne.Controllers
{
    [Route("api/about")]
    public class AboutController : ControllerBase
    {
        private IAboutService aboutService = new AboutServiceImpl();

        [HttpPost]
        [Route("adddata")]
        public async Task<string> AddHomeData(AboutDto aboutDto)
        {
            return await aboutService.AddAboutPageData(aboutDto);
        }

        [HttpPut]
        [Route("update")]
        public Task<string> UpdateAboutData(AboutDto aboutDto, string id)
        {
            return aboutService.UpdateAboutPageData(aboutDto, id);
        }

        [HttpDelete]
        [Route("delete-image")]
        public Task<string> DeleteParticularImage(string id,int priority)
        {
            return aboutService.DeleteParticularImage(id,priority);
        }

        [HttpDelete]
        [Route("delete")]
        public Task<string> DeleteAboutData(string id)
        {
            return aboutService.DeleteAboutPageData(id);
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<AboutModel>> GetAllAboutPage() 
        {
            return await aboutService.GetAllAboutPageDetails();
        }

        [HttpGet]
        [Route("")]
        public AboutModel GetAboutPage(string id) 
        {
            return aboutService.GetAboutPageData(id);
        }

        [HttpGet]
        [Route("1")]
        public async Task<AboutModel> GetFirstAboutData()
        {
            List<AboutModel> allAboutPages = await aboutService.GetAllAboutPageDetails();
            return allAboutPages.FirstOrDefault();
        }

        [HttpPut]
        [Route("add-skills")]
        public Task<string> AddSkills(Dictionary<string,string> skills,string id)
        {
            return aboutService.AddSkills(skills, id);
        }

        [HttpPut]
        [Route("remove-skill")]
        public Task<string> RemoveSkill(string skill,string id)
        {
            return aboutService.RemoveSkill(skill, id);
        }

        [HttpPut]
        [Route("add-hobbies")]
        public Task<string> AddHobbies(List<string> hobbies, string id)
        {
            return aboutService.AddHobbies(hobbies, id);
        }

        [HttpPut]
        [Route("remove-hobbies")]
        public Task<string> RemoveHobby(string hobby,string id)
        {
            return aboutService.RemoveHobby(hobby, id);
        }
    }
}
