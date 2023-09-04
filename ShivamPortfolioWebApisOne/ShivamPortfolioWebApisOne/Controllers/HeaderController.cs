using Microsoft.AspNetCore.Mvc;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Service;
using ShivamPortfolioWebApisOne.Service.Implementation;

namespace ShivamPortfolioWebApisOne.Controllers
{
    [Route("api/header")]
    public class HeaderController : ControllerBase
    {
        private IHeaderService headerService = new HeaderServiceImpl();
        [HttpDelete]
        [Route("delete")]
        public Task<string> DeleteHeader(string id)
        {
            return headerService.DeleteHeaderLogo(id);
        }

        [HttpGet]
        [Route("")]
        public HeaderModel GetHeader(string id)
        {
            return headerService.GetHeader(id);
        }

        [HttpPost]
        [Route("addheader")]
        public Task<string> AddHeader(HeaderDto headerDto)
        {
            return headerService.AddHeaderLogo(headerDto);
        }

        [HttpPut]
        [Route("update")]
        public Task<string> UpdateHeader(HeaderDto headerDto,string id)
        {
            return headerService.UpdateHeaderLogo(headerDto,id);
        }

        [HttpGet]
        [Route("display-logo")]
        public HeaderModel DisplayLogo()
        {
            return headerService.DisplayLogo();
        }
    }
}
