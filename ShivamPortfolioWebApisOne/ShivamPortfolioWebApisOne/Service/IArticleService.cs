using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service
{
    public interface IArticleService
    {
        public Task<string> AddArticle(ArticleDto articleDto); 

        public Task<string> DeleteArticle(string id);

        public Task<string> UpdateArticle(ArticleDto articleDto,string id);

        public Task<string> DeleteArticleLogo(string id);

        public Task<List<ArticlesModel>> GetAllArticles();

        public ArticlesModel GetArticle(string id);

        public Task<string> RemoveLink(string id,string key);

        public Task<string> AddLinks(Dictionary<string, string> Link, string id);
    }
}
