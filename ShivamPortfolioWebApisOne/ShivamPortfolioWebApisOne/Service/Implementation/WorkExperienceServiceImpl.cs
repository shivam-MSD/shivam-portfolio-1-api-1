using MongoDB.Driver;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service.Implementation
{
    public class WorkExperienceServiceImpl : IWorkExperienceService
    {
        IMongoConnection _connection = new MongoConnectionImpl();
        IImageHelper<WorkExperienceDto> _imageHelper = new ImageHelperImpl<WorkExperienceDto>();
        public Task<string> AddWorkExperience(WorkExperienceDto workExperienceDto)
        {
            try
            {
                WorkExperienceModel workExperienceModel = new WorkExperienceModel();
                workExperienceModel.CompanyName = workExperienceDto.CompanyName;
                workExperienceModel.Description = workExperienceDto.Description;
                workExperienceModel.priority = workExperienceDto.CompanyPriority;
                workExperienceModel.NoOfMnthOrYearExperience = workExperienceDto.NoOfMnthOrYearExperience;
                workExperienceModel.MonthOrYearExperience = workExperienceDto.MonthOrYearExperience;
                workExperienceModel.PriorityImageDictionary = _imageHelper.AddOrUpdateImageWithPriority(new Dictionary<string, byte[]>(), workExperienceDto);
                workExperienceModel.Logo = _imageHelper.GetImageBytes(workExperienceDto.LogoImagePath);
                workExperienceModel.CompanyWebSiteLink = workExperienceDto.CompanyWebSiteLink;
                workExperienceModel.PositionName = workExperienceDto.PositionName;

                var collection = _connection.ConnectToMongoDb<WorkExperienceModel>(Constants.Constants.WorkExperienceCollection);

                return collection.InsertOneAsync(workExperienceModel).ContinueWith(_ => "Work experience Inserted Successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<string> DeleteParticularImage(string id, int priority)
        {
            try
            {
                var getWorkExperienceModel = GetWorkExperience(id);
                if (getWorkExperienceModel.PriorityImageDictionary != null)
                {
                    if (!getWorkExperienceModel.PriorityImageDictionary.ContainsKey(priority.ToString()))
                    {
                        throw new Exception("ProjectPriority not found. List of images remains as it is.");
                    }
                    getWorkExperienceModel.PriorityImageDictionary.Remove(priority.ToString());
                    if (getWorkExperienceModel.PriorityImageDictionary.Count == 0)
                    {
                        getWorkExperienceModel.PriorityImageDictionary = null;
                    }
                    var collection = _connection.ConnectToMongoDb<WorkExperienceModel>(Constants.Constants.WorkExperienceCollection);
                    var filter = Builders<WorkExperienceModel>.Filter.Eq("Id", id);
                    return collection.ReplaceOneAsync(filter, getWorkExperienceModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Image deleted Successfully");
                }
                else
                {
                    throw new Exception("Image and priority not found...");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<string> DeleteWorkExperience(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<WorkExperienceModel>(Constants.Constants.WorkExperienceCollection);
                var filter = Builders<WorkExperienceModel>.Filter.Eq("Id", id);
                if (collection.Find(filter).FirstOrDefault() == null)
                {
                    throw new Exception("Id does not exist..Data remains as it is.");
                }
                return collection.DeleteOneAsync(hm => hm.Id.Equals(id)).ContinueWith(_ => "Work experience deleted successfully");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<WorkExperienceModel>> GetAllWorkExperience()
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<WorkExperienceModel>(Constants.Constants.WorkExperienceCollection);
                var workExperienceDataDocument = await collection.FindAsync(_ => true);
                return workExperienceDataDocument.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public WorkExperienceModel GetWorkExperience(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<WorkExperienceModel>(Constants.Constants.WorkExperienceCollection);
                var filter = Builders<WorkExperienceModel>.Filter.Eq("Id", id);
                var getWorkExperienceModel = collection.Find(filter).FirstOrDefault();

                if (getWorkExperienceModel == null)
                {
                    throw new Exception("Id not found. Please enter correct Id.");
                }
                return getWorkExperienceModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message+"\nPlease enter valid Id.");
            }
        }

        public Task<string> RemoveLogo(string id)
        {
            try
            {
                var getWorkExperienceModel = GetWorkExperience(id);
                getWorkExperienceModel.Logo = null;

                var collection = _connection.ConnectToMongoDb<WorkExperienceModel>(Constants.Constants.WorkExperienceCollection);
                var filter = Builders<WorkExperienceModel>.Filter.Eq("Id", id);
                return collection.ReplaceOneAsync(filter, getWorkExperienceModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Logo removed successfully.");
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public Task<string> UpdateWorkExperience(WorkExperienceDto workExperienceDto, string id)
        {
            try
            {
                var getWorkExperienceModel = GetWorkExperience(id);
                getWorkExperienceModel.CompanyName = workExperienceDto.CompanyName;
                getWorkExperienceModel.Description = workExperienceDto.Description;
                getWorkExperienceModel.priority = workExperienceDto.CompanyPriority;
                getWorkExperienceModel.NoOfMnthOrYearExperience = workExperienceDto.NoOfMnthOrYearExperience;
                getWorkExperienceModel.MonthOrYearExperience = workExperienceDto.MonthOrYearExperience;
                getWorkExperienceModel.CompanyWebSiteLink = workExperienceDto.CompanyWebSiteLink;
                getWorkExperienceModel.PositionName = workExperienceDto.PositionName;

                if (workExperienceDto.ImagePath != null)
                {
                    getWorkExperienceModel.PriorityImageDictionary = _imageHelper.AddOrUpdateImageWithPriority(getWorkExperienceModel.PriorityImageDictionary, workExperienceDto);
                }
                if(workExperienceDto.LogoImagePath != null)
                {
                    getWorkExperienceModel.Logo = _imageHelper.GetImageBytes(workExperienceDto.LogoImagePath);
                }
                var collection = _connection.ConnectToMongoDb<WorkExperienceModel>(Constants.Constants.WorkExperienceCollection);
                var filter = Builders<WorkExperienceModel>.Filter.Eq("Id", id);
                return collection.ReplaceOneAsync(filter, getWorkExperienceModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Data updated successfully.");
            }
            catch (Exception ex)
            {
                return Task.FromResult("Id not found. Please enter correct Id.");
            }
        }
    }
}
