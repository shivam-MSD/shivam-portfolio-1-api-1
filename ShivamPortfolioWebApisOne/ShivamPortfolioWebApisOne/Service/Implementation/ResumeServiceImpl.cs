using MongoDB.Driver;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service.Implementation
{
    public class ResumeServiceImpl : IResumeService
    {
        IMongoConnection _connection = new MongoConnectionImpl();

        public Task<string> AddNewResume(ResumeDto resumeDto)
        {
            try
            {
                ResumeModel resumeModel = new ResumeModel();
                resumeModel.ResumeLink = resumeDto.ResumeFileLink;

                var collection = _connection.ConnectToMongoDb<ResumeModel>(Constants.Constants.ResumeCollection);

                return collection.InsertOneAsync(resumeModel).ContinueWith(_ => "Resume data added successfully");
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        public Task<string> DeleteResume(string resumeId)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<ResumeModel>(Constants.Constants.ResumeCollection);
                var filter = Builders<ResumeModel>.Filter.Eq("Id", resumeId);

                if (collection.Find(filter).FirstOrDefault() == null)
                {
                    throw new Exception("Id not found. Please enter correct Id.");
                }

                return collection.DeleteOneAsync(res => res.Id.Equals(resumeId)).ContinueWith(_ => "About Data deleted successfully");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ResumeModel>> GetAllResume()
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<ResumeModel>(Constants.Constants.ResumeCollection);
                var resumeDocument = await collection.FindAsync(_ => true);

                if(resumeDocument == null)
                {
                    return null;
                }
                return resumeDocument.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResumeModel GetResume(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<ResumeModel>(Constants.Constants.ResumeCollection);
                var filter = Builders<ResumeModel>.Filter.Eq("Id", id);

                var getResumeModel = collection.Find(filter).FirstOrDefault();

                if (getResumeModel != null)
                {
                    return getResumeModel;
                }
                else
                {
                    throw new Exception("Id not found. Please enter correct Id.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> UpdateResume(ResumeDto resumeDto, string id)
        {
            try
            {
                var getResumeModel = GetResume(id);

                getResumeModel.ResumeLink = resumeDto.ResumeFileLink;

                var collection = _connection.ConnectToMongoDb<ResumeModel>(Constants.Constants.ResumeCollection);
                var filter = Builders<ResumeModel>.Filter.Eq("Id", id);

                return collection.ReplaceOneAsync(filter, getResumeModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Data updated successfully.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
