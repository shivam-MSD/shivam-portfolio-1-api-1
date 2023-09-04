using Amazon.Runtime.Internal.Transform;
using MongoDB.Driver;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service.Implementation
{
    public class ArticlesServiceImpl : IArticleService
    {
        IMongoConnection _connection = new MongoConnectionImpl();
        IImageHelper<ArticleDto> _imageHelper = new ImageHelperImpl<ArticleDto>();
        public Task<string> AddArticle(ArticleDto articleDto)
        {
            try
            {
                ArticlesModel articlesModel = new ArticlesModel();
                articlesModel.Title = articleDto.Title;
                articlesModel.Description = articleDto.Description;
                articlesModel.Priority = articleDto.ImagePriority;
                articlesModel.Image = _imageHelper.GetImageBytes(articleDto.ImagePath);
                articlesModel.Link = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                articlesModel.Link = articleDto.Link;
                //articlesModel.Link = AddLinks(articleDto.Link);

                var collection = _connection.ConnectToMongoDb<ArticlesModel>(Constants.Constants.ArticleCollection);
                return collection.InsertOneAsync(articlesModel).ContinueWith(_ => "Data Inserted Successfully");
            }
            catch(Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        public Task<string> DeleteArticle(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<ArticlesModel>(Constants.Constants.ArticleCollection);
                var filter = Builders<ArticlesModel>.Filter.Eq("Id", id);
                if (collection.Find(filter).FirstOrDefault() == null)
                {
                    throw new Exception("Id does not exist..Data remains as it is.");
                }
                return collection.DeleteOneAsync(abm => abm.Id.Equals(id)).ContinueWith(_ => "Article deleted successfully");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<string> DeleteArticleLogo(string id)
        {
            try
            {
                var getArticleModel = GetArticle(id);
                getArticleModel.Image = null;

                var collection = _connection.ConnectToMongoDb<ArticlesModel>(Constants.Constants.ArticleCollection);
                var filter = Builders<ArticlesModel>.Filter.Eq("Id", id);
                return collection.ReplaceOneAsync(filter, getArticleModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Logo Deleted successfully."); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ArticlesModel>> GetAllArticles()
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<ArticlesModel>(Constants.Constants.ArticleCollection);
                var articleDataDocument = await collection.FindAsync(_ => true);
                return articleDataDocument.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArticlesModel GetArticle(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<ArticlesModel>(Constants.Constants.ArticleCollection);
                var filter = Builders<ArticlesModel>.Filter.Eq("Id", id);
                var getArticleModel = collection.Find(filter).FirstOrDefault();

                if (getArticleModel == null)
                {
                    throw new Exception("Id not found. Please enter correct Id.");
                }
                return getArticleModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nPlease enter valid Id.");
            }

        }

        public Task<string> RemoveLink(string key, string id)
        {
            try
            {
                var getArticleModel = GetArticle(id);
                if(getArticleModel != null && getArticleModel.Link != null && getArticleModel.Link.ContainsKey(key.Trim())) 
                {
                    getArticleModel.Link.Remove(key.Trim());
                    if(getArticleModel.Link.Count == 0)
                    {
                        getArticleModel.Link = null;
                    }
                    var collection = _connection.ConnectToMongoDb<ArticlesModel>(Constants.Constants.ArticleCollection);
                    var filter = Builders<ArticlesModel>.Filter.Eq("Id", id);
                    return collection.ReplaceOneAsync(filter, getArticleModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Link removed successfully."); ;
                }
                else
                {
                    throw new Exception("No link found.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<string> AddLinks(Dictionary<string, string> Link, string id)
        {
            try
            {
                var getArticleModel = GetArticle(id);
                Dictionary<string, string> presentLinks = getArticleModel.Link;
                if(Link != null)
                {
                    Link = Link.Where(entry => !string.IsNullOrWhiteSpace(entry.Key) && !string.IsNullOrWhiteSpace(entry.Value))
                    .ToDictionary(entry => entry.Key, entry => entry.Value);

                    if (Link.Count > 0)
                    {
                        if(presentLinks == null)
                        {
                            presentLinks = new Dictionary<string, string>();
                        }

                        getArticleModel.Link = presentLinks.Union(Link)
                            .ToDictionary(entry => entry.Key.Trim(),entry => entry.Value.Trim());

                        var collection = _connection.ConnectToMongoDb<ArticlesModel>(Constants.Constants.ArticleCollection);
                        var filter = Builders<ArticlesModel>.Filter.Eq("Id", id);
                        return collection.ReplaceOneAsync(filter, getArticleModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Links added successfully."); ;
                    }
                    else
                    {
                        throw new Exception("Link cannot be empty or null. Please add the link");
                    }
                }
                else
                {
                    throw new Exception("Link cannot be empty or null. Please add the link");
                }
            }
            catch(Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        public Task<string> UpdateArticle(ArticleDto articleDto, string id)
        {
            try
            { 
            
                var getArticleModel = GetArticle(id);
                getArticleModel.Title = articleDto.Title;
                getArticleModel.Description = articleDto.Description;
                getArticleModel.Priority = articleDto.ImagePriority;
                getArticleModel.Image = _imageHelper.GetImageBytes(articleDto.ImagePath);

                if(articleDto.Link != null)
                {
                    AddLinks(articleDto.Link, id);
                }
                getArticleModel.Link = articleDto.Link;
                //getArticleModel.Link = UpdateLinks(articleDto.Link, getArticleModel.Link);

                var collection = _connection.ConnectToMongoDb<ArticlesModel>(Constants.Constants.ArticleCollection);
                var filter = Builders<ArticlesModel>.Filter.Eq("Id", id);
                return collection.ReplaceOneAsync(filter, getArticleModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Data updated successfully."); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //private Dictionary<string, string> AddLinks(Dictionary<string, string> Link)
        //{
        //    if (Link != null)
        //    {
        //        Link = Link
        //        .Where(entry => !string.IsNullOrWhiteSpace(entry.Key) && !string.IsNullOrWhiteSpace(entry.Value))
        //        .ToDictionary(entry => entry.Key, entry => entry.Value);

        //        if(Link.Count > 0)
        //        {
        //            return Link;
        //        }
        //    }
        //    return null;
        //}

        //private Dictionary<string, string> UpdateLinks(Dictionary<string, string> Link, Dictionary<string, string> presentLinks)
        //{
        //    if(Link != null)
        //    {
        //        Link = Link
        //        .Where(entry => !string.IsNullOrWhiteSpace(entry.Key) && !string.IsNullOrWhiteSpace(entry.Value))
        //        .ToDictionary(entry => entry.Key, entry => entry.Value);

        //        if (Link.Count > 0)
        //        {
        //            if (presentLinks == null)
        //            {
        //                presentLinks = new Dictionary<string, string>();
        //            }
        //            presentLinks.Concat(Link).ToDictionary(link => link.Key, link => link.Value);
        //        }
        //    }

        //    return presentLinks;
        //}
    }
}
