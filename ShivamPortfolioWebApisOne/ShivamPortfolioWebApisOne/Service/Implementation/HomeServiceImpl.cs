using Microsoft.AspNetCore.Mvc;
using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Dtos;
using System.Linq;
using SharpCompress.Common;
using System.IO;
using MongoDB.Driver;
using ShivamPortfolioWebApisOne.Constants;
using ShivamPortfolioWebApisOne.Controllers;

namespace ShivamPortfolioWebApisOne.Service.Implementation
{
    public class HomeServiceImpl : IHomeService
    {
        IMongoConnection _connection = new MongoConnectionImpl();
        IImageHelper<HomeDto> _imageHelper = new ImageHelperImpl<HomeDto>();

        public async Task<string> AddHomePageData(HomeDto homeDto)
        {
            try
            {
                HomeModel homeModel = new HomeModel();
                homeModel.Name = homeDto.Name;
                homeModel.Description = homeDto.Description;
                homeModel.PriorityImageDictionary = _imageHelper.AddOrUpdateImageWithPriority(new Dictionary<string, byte[]>(), homeDto);

                var collection =  _connection.ConnectToMongoDb<HomeModel>(Constants.Constants.HomeCollection);

                return await collection.InsertOneAsync(homeModel).ContinueWith(_ => "Home Data Inserted Successfully");

            }
            catch(Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        public async Task<string> DeleteHomePageData(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<HomeModel>(Constants.Constants.HomeCollection);
                var filter = Builders<HomeModel>.Filter.Eq("Id", id);
                if (collection.Find(filter).FirstOrDefault() == null)
                {
                    throw new Exception("Id does not exist..Data remains as it is.");
                }
                return await collection.DeleteOneAsync(hm => hm.Id.Equals(id)).ContinueWith(_ => "Home Data deleted successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> DeleteParticularImage(string id, int priority)
        {
            try
            {

                var getHomeModel = GetHomePageData(id);
                if (getHomeModel.PriorityImageDictionary != null)
                {
                    if (!getHomeModel.PriorityImageDictionary.ContainsKey(priority.ToString()))
                    {
                        throw new Exception("ImagePriority not found. List of images remains as it is.");
                    }
                    getHomeModel.PriorityImageDictionary.Remove(priority.ToString());
                    if(getHomeModel.PriorityImageDictionary.Count == 0)
                    {
                        getHomeModel.PriorityImageDictionary = null;
                    }
                    var collection = _connection.ConnectToMongoDb<HomeModel>(Constants.Constants.HomeCollection);
                    var filter = Builders<HomeModel>.Filter.Eq("Id", id);
                    return await collection.ReplaceOneAsync(filter, getHomeModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Image deleted Successfully");
                }
                else
                {
                    throw new Exception("Image and priority not found...");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<HomeModel>> GetAllHomePageDetails()
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<HomeModel>(Constants.Constants.HomeCollection);
                var homeDataDocument = await collection.FindAsync(_ => true);
                return homeDataDocument.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public HomeModel GetHomePageData(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<HomeModel>(Constants.Constants.HomeCollection);
                var filter = Builders<HomeModel>.Filter.Eq("Id", id);
                var getHomeModel = collection.Find(filter).FirstOrDefault();

                if (getHomeModel == null)
                {
                    throw new Exception("Id not found. Please enter correct Id.");
                }
                return getHomeModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Please enter valid Id.");
            }
        }

        public async Task<string> UpdateHomeData(HomeDto homeDto,string id)
        {
           try
            {
                var getHomeModel = GetHomePageData(id);
                getHomeModel.Name = homeDto.Name;
                getHomeModel.Description = homeDto.Description;
                
                if(homeDto.ImagePath != null)
                {
                    getHomeModel.PriorityImageDictionary = _imageHelper.AddOrUpdateImageWithPriority(getHomeModel.PriorityImageDictionary, homeDto);
                }
                var collection = _connection.ConnectToMongoDb<HomeModel>(Constants.Constants.HomeCollection);
                var filter = Builders<HomeModel>.Filter.Eq("Id", id);
                return await collection.ReplaceOneAsync(filter, getHomeModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Data updated successfully.");
            }
            catch (Exception ex)
            {
                return await Task.FromResult("Id not found. Please enter correct Id.");
            }
        }
    }
}
//string AboutPageCollection = "dummycollection";
//HomeModel homeModel = new HomeModel("Shivam Patel","I am a software engineer", binaryContent);
//var collection = _connection.ConnectToMongoDb<Data>(AboutPageCollection);
