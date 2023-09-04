using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;
using ShivamPortfolioWebApisOne.Constants;
using MongoDB.Driver;

namespace ShivamPortfolioWebApisOne.Service.Implementation
{
    public class HeaderServiceImpl : IHeaderService
    {
        IMongoConnection _connection = new MongoConnectionImpl();
        IImageHelper<HeaderDto> imageHelper = new ImageHelperImpl<HeaderDto>();
        public Task<string> AddHeaderLogo(HeaderDto headerDto)
        {
            try
            {
                HeaderModel headerModel = new HeaderModel();
                headerModel.Priority = headerDto.ImagePriority;
                headerModel.Logo = imageHelper.GetImageBytes(headerDto.ImagePath);

                var collection = _connection.ConnectToMongoDb<HeaderModel>(Constants.Constants.HeaderCollection);

                return collection.InsertOneAsync(headerModel).ContinueWith(_ => "Header Data Inserted Successfully");
            }
            catch(Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        public Task<string> DeleteHeaderLogo(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<HeaderModel>(Constants.Constants.HeaderCollection);
                var filter = Builders<HeaderModel>.Filter.Eq("Id", id);

                if(collection.Find(filter).FirstOrDefault() == null)
                {
                    throw new Exception("Id does not exist..Data remains as it is.");
                }
                return collection.DeleteOneAsync(hd => hd.Id.Equals(id)).ContinueWith(_ => "Header Data deleted successfully");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public HeaderModel DisplayLogo()
        {
            try
            {
                List<HeaderModel> getAllHearderModel = GetAllHeader().Result;
                if(getAllHearderModel !=  null && getAllHearderModel.Count > 0)
                {
                    List<HeaderModel> headerModelAsc =  getAllHearderModel.OrderBy(header => header.Priority).ToList();

                    //return getAllHearderModel.FirstOrDefault();
                    return headerModelAsc.FirstOrDefault();
                }
                else
                {
                    throw new Exception("No header data found");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<HeaderModel>> GetAllHeader()
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<HeaderModel>(Constants.Constants.HeaderCollection);
                var headerDocument = await collection.FindAsync(_ => true);
                return headerDocument.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HeaderModel GetHeader(string id)
        {

            try
            {
                var collection = _connection.ConnectToMongoDb<HeaderModel>(Constants.Constants.HeaderCollection);
                var filter = Builders<HeaderModel>.Filter.Eq("Id", id);
                var getHeaderModel = collection.Find(filter).FirstOrDefault();

                if(getHeaderModel == null) 
                {
                    throw new Exception("Id not found. Please enter correct Id");
                }
                return getHeaderModel;
            }
            catch(Exception ex)
            {
                throw new Exception("Please enter valid Id.");
            }
        }

        public Task<string> UpdateHeaderLogo(HeaderDto headerDto, string id)
        {
            try
            {
                var getHeaderModel = GetHeader(id);
                getHeaderModel.Priority = headerDto.ImagePriority;

                if(headerDto.ImagePath != null)
                {
                    getHeaderModel.Logo = imageHelper.GetImageBytes(headerDto.ImagePath);
                }
                var collection = _connection.ConnectToMongoDb<HeaderModel>(Constants.Constants.HeaderCollection);
                var filter = Builders<HeaderModel>.Filter.Eq("Id", id);

                return collection.ReplaceOneAsync(filter,getHeaderModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Data updated successfully.");
            }
            catch (Exception ex)
            {
                return Task.FromResult("Id not found. Please enter correct Id.");
            }
        }
    }
}
