using Microsoft.AspNetCore.Mvc;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Service;
using ShivamPortfolioWebApisOne.Service.Implementation;

namespace ShivamPortfolioWebApisOne.Controllers
{
    [Route("api/articles")]
    public class ArticleController : ControllerBase
    {
        IArticleService articleService = new ArticlesServiceImpl();

        [HttpPost]
        [Route("addarticle")]
        public Task<string> AddArticle(ArticleDto articleDto)
        {
            return articleService.AddArticle(articleDto);
        }

        [HttpPut]
        [Route("updatearticle")]
        public Task<string> UpdateArticle(ArticleDto articleDto, string id)
        {
            return articleService.UpdateArticle(articleDto, id);
        }

        [HttpDelete]
        [Route("delete")]
        public Task<string> DeleteArticle(string id)
        {
            return articleService.DeleteArticle(id);
        }

        [HttpDelete]
        [Route("delete-logo")]
        public Task<string> DeleteLogo(string id)
        {
            return articleService.DeleteArticleLogo(id);
        }

        [HttpGet]
        [Route("all")]
        public async Task<List<ArticlesModel>> GetAllArticle()
        {
            return await articleService.GetAllArticles();
        }

        [HttpGet]
        [Route("")]
        public ArticlesModel GetArticle(string id)
        {
            return articleService.GetArticle(id);
        }

        [HttpPut]
        [Route("remove-link")]
        public Task<string> RemoveLink(string id,string key)
        {
            return articleService.RemoveLink(id,key);
        }

        [HttpPut]
        [Route("add-Link")]
        public Task<string> AddLinks(Dictionary<string, string> Link, string id)
        {
            return articleService.AddLinks(Link, id);
        }
    }
}
