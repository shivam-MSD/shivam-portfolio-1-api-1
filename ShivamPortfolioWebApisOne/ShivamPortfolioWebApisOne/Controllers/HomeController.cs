using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Service;
using ShivamPortfolioWebApisOne.Service.Implementation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShivamPortfolioWebApisOne.Controllers
{
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private IHomeService homeService = new HomeServiceImpl();

        [HttpPost]
        [Route("adddata")]
        public async Task<string> AddHomeData(HomeDto homeDto)
        {
            return await homeService.AddHomePageData(homeDto);
        }

        [HttpPut]
        [Route("update")]
        public async Task<string> UpdateHomeData(HomeDto homeDto,string id)
        {
            return await homeService.UpdateHomeData(homeDto, id);
        }

        [HttpDelete]
        [Route("delete-image")]
        public async Task<string> DeleteParticularImage(string id,int priority)
        {
            return await homeService.DeleteParticularImage(id, priority);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<string> DeleteHomeData(string id)
        {
            return await homeService.DeleteHomePageData(id);
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<HomeModel>> GetAllHomePages()
        {
            return await homeService.GetAllHomePageDetails();
        }

        [HttpGet]
        [Route("")]
        public HomeModel GetHomePage(string id)
        {
            return homeService.GetHomePageData(id);
        }

        [HttpGet]
        [Route("1")]
        public HomeModel GetFirstHomePage()
        {
            return homeService.GetAllHomePageDetails().GetAwaiter().GetResult().FirstOrDefault();
        }
    }
}
