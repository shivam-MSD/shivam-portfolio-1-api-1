using MongoDB.Driver;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service.Implementation
{
    public class EducationServiceImpl : IEducationService
    {
        IMongoConnection _connection = new MongoConnectionImpl();
        IImageHelper<EducationDto> _imageHelper = new ImageHelperImpl<EducationDto>();

        public Task<string> AddCareerDescription(string id, string careerDescription)
        {
            try
            {
                var getEducationModel = GetEducationData(id);

                if(careerDescription == null)
                {
                    throw new Exception("Cannot add null Description ");
                }
                getEducationModel.MyCareerDescription = careerDescription.Replace("\r\n", "\n"); ;

                var collection = _connection.ConnectToMongoDb<EducationModel>(Constants.Constants.EducationCollection);
                var filter = Builders<EducationModel>.Filter.Eq("Id", id);
                return collection.ReplaceOneAsync(filter, getEducationModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Data updated successfully.");
            }
            catch(Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        public Task<string> AddEducationData(EducationDto educationDto)
        {
            try
            {
                EducationModel educationModel = new EducationModel();
                educationModel.SchoolName = educationDto.SchoolName;
                educationModel.EducationBoard = educationDto.EducationBoard;
                educationModel.Description = educationDto.Description;
                educationModel.FieldName = educationDto.FieldName;
                educationModel.Grades = educationDto.Grades;
                educationModel.TypeOfSchool = educationDto.TypeOfSchool;
                educationModel.PriorityImageDictionary = _imageHelper.AddOrUpdateImageWithPriority(new Dictionary<string, byte[]>(), educationDto);
                educationModel.MyCareerDescription = educationDto.MyCareerDescription.Replace("\r\n", "\n"); ;

                var collection = _connection.ConnectToMongoDb<EducationModel>(Constants.Constants.EducationCollection);

                return collection.InsertOneAsync(educationModel).ContinueWith(_ => "Education Data inserted successfully");
            }
            catch (Exception ex) 
            {
                return Task.FromResult(ex.Message);
            }
        }

        public Task<string> DeleteCareerDescription(string id)
        {
            try
            {
                var getEducationModel = GetEducationData(id);

                getEducationModel.MyCareerDescription = null;

                var collection = _connection.ConnectToMongoDb<EducationModel>(Constants.Constants.EducationCollection);
                var filter = Builders<EducationModel>.Filter.Eq("Id", id);
                return collection.ReplaceOneAsync(filter, getEducationModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Data updated successfully.");
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        public Task<string> DeleteEducationData(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<EducationModel>(Constants.Constants.EducationCollection);
                var filter = Builders<EducationModel>.Filter.Eq("Id", id);
                if (collection.Find(filter).FirstOrDefault() == null)
                {
                    throw new Exception("Id does not exist..Data remains as it is.");
                }
                return collection.DeleteOneAsync(ed => ed.Id.Equals(id)).ContinueWith(_ => "Education Data deleted successfully");
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
                var getEducationModel = GetEducationData(id);
                if (getEducationModel.PriorityImageDictionary != null)
                {
                    if (!getEducationModel.PriorityImageDictionary.ContainsKey(priority.ToString()))
                    {
                        throw new Exception("ProjectPriority not found. List of images remains as it is.");
                    }
                    getEducationModel.PriorityImageDictionary.Remove(priority.ToString());
                    if (getEducationModel.PriorityImageDictionary.Count == 0)
                    {
                        getEducationModel.PriorityImageDictionary = null;
                    }
                    var collection = _connection.ConnectToMongoDb<EducationModel>(Constants.Constants.EducationCollection);
                    var filter = Builders<EducationModel>.Filter.Eq("Id", id);
                    return collection.ReplaceOneAsync(filter, getEducationModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Image deleted Successfully");
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

        public async Task<List<EducationModel>> GetAllEducationData()
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<EducationModel>(Constants.Constants.EducationCollection);
                var educationDataDocument = await collection.FindAsync(_ => true);
                return educationDataDocument.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public EducationModel GetEducationData(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<EducationModel>(Constants.Constants.EducationCollection);
                var filter = Builders<EducationModel>.Filter.Eq("Id", id);
                var getEducationModel = collection.Find(filter).FirstOrDefault();

                if (getEducationModel == null)
                {
                    throw new Exception("Id not found. Please enter correct Id.");
                }
                return getEducationModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nPlease enter valid Id.");
            }
        }

        public Task<string> UpdateEducationData(EducationDto educationDto, string id)
        {
            try
            {
                var getEducationModel = GetEducationData(id);
                getEducationModel.SchoolName = educationDto.SchoolName;
                getEducationModel.EducationBoard = educationDto.EducationBoard;
                getEducationModel.Description = educationDto.Description;
                getEducationModel.FieldName = educationDto.FieldName;
                getEducationModel.Grades = educationDto.Grades;
                getEducationModel.TypeOfSchool = educationDto.TypeOfSchool;

                if (educationDto.ImagePath != null)
                {
                    getEducationModel.PriorityImageDictionary = _imageHelper.AddOrUpdateImageWithPriority(getEducationModel.PriorityImageDictionary, educationDto);
                }

                if(educationDto.MyCareerDescription != null)
                {
                    getEducationModel.MyCareerDescription = educationDto.MyCareerDescription.Replace("\r\n", "\n"); ;
                    //getEducationModel.MyCareerDescription.Replace("\r\n", "\n");
                }

                var collection = _connection.ConnectToMongoDb<EducationModel>(Constants.Constants.EducationCollection);
                var filter = Builders<EducationModel>.Filter.Eq("Id", id);
                return collection.ReplaceOneAsync(filter, getEducationModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Data updated successfully.");
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }
    }
}
