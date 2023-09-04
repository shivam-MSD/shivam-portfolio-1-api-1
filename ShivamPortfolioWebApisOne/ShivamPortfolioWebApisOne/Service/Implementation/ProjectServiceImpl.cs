using MongoDB.Driver;
using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service.Implementation
{
    public class ProjectServiceImpl : IProjectsService
    {
        IMongoConnection _connection = new MongoConnectionImpl();
        IImageHelper<ProjectsDto> _imageHelper = new ImageHelperImpl<ProjectsDto>();

        public Task<string> AddProject(ProjectsDto projectsDto)
        {
            try
            {
                ProjectsModel projectsModel = new ProjectsModel();
                projectsModel.ProjectName = projectsDto.ProjectName;
                projectsModel.ProjectPriority = projectsDto.ProjectPriority;
                projectsModel.Description = projectsDto.Description;
                projectsModel.TechnologyUsed = projectsDto.TechnologyUsed;
                projectsModel.PriorityImageDictionary = _imageHelper.AddOrUpdateImageWithPriority(new Dictionary<string, byte[]>(), projectsDto);
                projectsModel.Database = projectsDto.Database;

                var collection = _connection.ConnectToMongoDb<ProjectsModel>(Constants.Constants.ProjectCollection);

                return collection.InsertOneAsync(projectsModel).ContinueWith(_ => "Project added successfully");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<string> DeleteParticularImage(string id, int priority)
        {
            try
            {

                var getProjectModel = GetProject(id);
                if (getProjectModel.PriorityImageDictionary != null)
                {
                    if (!getProjectModel.PriorityImageDictionary.ContainsKey(priority.ToString()))
                    {
                        throw new Exception("ProjectPriority not found. List of images remains as it is.");
                    }
                    getProjectModel.PriorityImageDictionary.Remove(priority.ToString());
                    if (getProjectModel.PriorityImageDictionary.Count == 0)
                    {
                        getProjectModel.PriorityImageDictionary = null;
                    }
                    var collection = _connection.ConnectToMongoDb<ProjectsModel>(Constants.Constants.ProjectCollection);
                    var filter = Builders<ProjectsModel>.Filter.Eq("Id", id);
                    return collection.ReplaceOneAsync(filter, getProjectModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Image deleted Successfully");
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

        public Task<string> DeleteProject(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<ProjectsModel>(Constants.Constants.ProjectCollection);
                var filter = Builders<ProjectsModel>.Filter.Eq("Id", id);
                if(collection.Find(filter).FirstOrDefault() == null)
                {
                    throw new Exception("Id does not exist..Data remains as it is.");
                }
                return collection.DeleteOneAsync(pr => pr.Id.Equals(id)).ContinueWith(_ => "Project deleted successfully");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ProjectsModel>> GetAllProjects()
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<ProjectsModel>(Constants.Constants.ProjectCollection);
                var projectDocument = await collection.FindAsync(_ => true);
                return projectDocument.ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ProjectsModel GetProject(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<ProjectsModel>(Constants.Constants.ProjectCollection);
                var filter = Builders<ProjectsModel>.Filter.Eq("Id", id);
                var getProjectModel = collection.Find(filter).FirstOrDefault();

                if (getProjectModel == null)
                {
                    throw new Exception("Id not found. Please enter correct Id.");
                }
                return getProjectModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message+"\nPlease enter valid Id.");
            }
        }

        public Task<string> UpdateProject(ProjectsDto projectsDto, string id)
        {
            try
            {
                var getProjectModel = GetProject(id);
                getProjectModel.ProjectName = projectsDto.ProjectName;
                getProjectModel.ProjectPriority = projectsDto.ProjectPriority;
                getProjectModel.Description = projectsDto.Description;
                getProjectModel.TechnologyUsed = UpdateTechnology(projectsDto.TechnologyUsed);
                getProjectModel.Database = projectsDto.Database;

                if(projectsDto.ImagePath != null)
                {
                    getProjectModel.PriorityImageDictionary = _imageHelper.AddOrUpdateImageWithPriority(getProjectModel.PriorityImageDictionary, projectsDto);
                }
                var collection = _connection.ConnectToMongoDb<ProjectsModel>(Constants.Constants.ProjectCollection);
                var filter = Builders<ProjectsModel>.Filter.Eq("Id", id);
                return collection.ReplaceOneAsync(filter, getProjectModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Data updated successfully.");
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }

        //private List<string> AddTechnologyUsed(List<string> TechnologyUsed)
        //{
        //    if (TechnologyUsed != null)
        //    {
        //        TechnologyUsed.RemoveAll(technology => technology == null);

        //        if (TechnologyUsed.Count > 0)
        //        {
        //            return TechnologyUsed;
        //        }
        //        return null;
        //    }
        //    return null;
        //}

        private List<string> UpdateTechnology(List<string> AddTechnology)
        {
            if (AddTechnology != null)
            {
                AddTechnology.RemoveAll(hobby => hobby == null);
                if (AddTechnology.Count == 0)
                {
                    return null;
                }
                return AddTechnology;
            }
            return null;
        }
        
    }
}
