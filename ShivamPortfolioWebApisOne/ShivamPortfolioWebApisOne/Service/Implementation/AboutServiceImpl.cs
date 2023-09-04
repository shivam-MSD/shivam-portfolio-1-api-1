using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;
using MongoDB.Driver;

namespace ShivamPortfolioWebApisOne.Service.Implementation
{
    public class AboutServiceImpl : IAboutService
    {
        IMongoConnection _connection = new MongoConnectionImpl();
        IImageHelper<AboutDto> _imageHelper = new ImageHelperImpl<AboutDto>();

        public async Task<string> AddAboutPageData(AboutDto aboutDto)
        {
            try
            {
                AboutModel aboutModel = new AboutModel();
                aboutModel.Name = aboutDto.Name;
                aboutModel.AboutDescription = aboutDto.AboutDescription != null
                ? aboutDto.AboutDescription.Replace("\r\n", "\n")
                : aboutDto.AboutDescription;
                aboutModel.Skills  = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                aboutModel.Skills = aboutDto.Skills;
                //aboutModel.Skills = AddLinks(aboutDto.Skills);
                //aboutModel.Hobbies = AddHobbies(aboutDto.Hobbies);
                aboutModel.Hobbies = aboutDto.Hobbies;
                aboutModel.AboutCareerDescription = aboutDto.AboutCareerDescription != null
                ? aboutDto.AboutCareerDescription.Replace("\r\n", "\n")
                : aboutDto.AboutCareerDescription;

                aboutModel.PriorityImageDictionary = _imageHelper.AddOrUpdateImageWithPriority(new Dictionary<string, byte[]>(), aboutDto);

                var collection = _connection.ConnectToMongoDb<AboutModel>(Constants.Constants.AboutCollection);

                return await collection.InsertOneAsync(aboutModel).ContinueWith(_ => "Data Inserted Successfully");

            }
            catch(Exception ex) 
            {
                return await Task.FromResult(ex.Message);
            }
        }

        public Task<string> DeleteAboutPageData(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<AboutModel>(Constants.Constants.AboutCollection);
                var filter = Builders<AboutModel>.Filter.Eq("Id", id);

                if (collection.Find(filter).FirstOrDefault() == null)
                {
                    throw new Exception("Id does not exist..Data remains as it is.");
                }
                return collection.DeleteOneAsync(abm => abm.Id.Equals(id)).ContinueWith(_ => "About Data deleted successfully");
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
                var getAboutPageModel = GetAboutPageData(id);
                if (getAboutPageModel.PriorityImageDictionary != null)
                {
                    if (!getAboutPageModel.PriorityImageDictionary.ContainsKey(priority.ToString()))
                    {
                        throw new Exception("ProjectPriority not found. List of images remains as it is.");
                    }
                    getAboutPageModel.PriorityImageDictionary.Remove(priority.ToString());
                    if (getAboutPageModel.PriorityImageDictionary.Count == 0)
                    {
                        getAboutPageModel.PriorityImageDictionary = null;
                    }
                    var collection = _connection.ConnectToMongoDb<AboutModel>(Constants.Constants.AboutCollection);
                    var filter = Builders<AboutModel>.Filter.Eq("Id", id);
                    return collection.ReplaceOneAsync(filter, getAboutPageModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Image deleted Successfully");
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

        public AboutModel GetAboutPageData(string id)
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<AboutModel>(Constants.Constants.AboutCollection);
                var filter = Builders<AboutModel>.Filter.Eq("Id", id);
                var getAboutModel = collection.Find(filter).FirstOrDefault();

                if (getAboutModel == null)
                {
                    throw new Exception("Id not found. Please enter correct Id.");
                }
                return getAboutModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "\nPlease enter valid Id.");
            }
        }

        /// <summary>
        /// This method is used to get all the about page details
        /// </summary>
        /// <returns></returns>
        public async Task<List<AboutModel>> GetAllAboutPageDetails()
        {
            try
            {
                var collection = _connection.ConnectToMongoDb<AboutModel>(Constants.Constants.AboutCollection);
                var aboutDataDocument = await collection.FindAsync(_ => true);
                return aboutDataDocument.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is used to update the about page data
        /// </summary>
        /// <param name="aboutDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> UpdateAboutPageData(AboutDto aboutDto, string id)
        {
            try
            {
                var getAboutPageModel = GetAboutPageData(id);
                getAboutPageModel.Name = aboutDto.Name != null ? aboutDto.Name : getAboutPageModel.Name;
                getAboutPageModel.AboutDescription = aboutDto.AboutDescription != null
                    ? aboutDto.AboutDescription.Replace("\r\n", "\n")
                    : getAboutPageModel.AboutDescription;
                //getAboutPageModel.Hobbies = UpdateHobbies(aboutDto.Hobbies);
                //getAboutPageModel.Skills = AddLinks(aboutDto.Skills,getAboutPageModel.Skills);
                getAboutPageModel.AboutCareerDescription = aboutDto.AboutCareerDescription != null
                    ? aboutDto.AboutCareerDescription.Replace("\r\n", "\n")
                    : getAboutPageModel.AboutCareerDescription;

                if (aboutDto.ImagePath != null)
                {
                    getAboutPageModel.PriorityImageDictionary = _imageHelper.AddOrUpdateImageWithPriority(getAboutPageModel.PriorityImageDictionary, aboutDto);
                }

                var collection = _connection.ConnectToMongoDb<AboutModel>(Constants.Constants.AboutCollection);
                var filter = Builders<AboutModel>.Filter.Eq("Id", id);
                return collection.ReplaceOneAsync(filter,getAboutPageModel,new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Data updated successfully.");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// This method is used to add the skills
        ///// </summary>
        ///// <param name="skills"></param>
        ///// <returns></returns>
        //private Dictionary<string,string> AddLinks(Dictionary<string,string> addSkills)
        //{
        //    if (addSkills != null)
        //    {
        //        //addSkills = addSkills
        //        //.Where(entry => !string.IsNullOrWhiteSpace(entry.Key) && !string.IsNullOrWhiteSpace(entry.Value))
        //        //.ToDictionary(entry => entry.Key, entry => entry.Value);

        //        if(addSkills.Count > 0)
        //        {
        //            return addSkills;
        //        }
        //    }
        //    return null;
        //}

        /// <summary>
        /// This method ins used to update the removeSkill in the about page.
        /// </summary>
        /// <param name="addSkills"></param>
        /// <param name="presentSkills"></param>
        /// <returns></returns>
        //private Dictionary<string, string> AddLinks(Dictionary<string, string> addSkills,Dictionary<string,string> presentSkills)
        public Task<string> AddSkills(Dictionary<string, string> addSkills,string id)
        {
            //if (addSkills != null)
            //{
            //    //addSkills = addSkills
            //    //.Where(entry => !string.IsNullOrWhiteSpace(entry.Key) && !string.IsNullOrWhiteSpace(entry.Value))
            //    //.ToDictionary(entry => entry.Key, entry => entry.Value);

            //    if(addSkills.Count > 0)
            //    {
            //        return addSkills;
            //    }
            //    return null;
            //}
            //return null;

            try
            {
                var getAboutPageModel = GetAboutPageData(id);
                Dictionary<string, string> presentSkills = getAboutPageModel.Skills;
                if (addSkills != null)
                {
                    addSkills = addSkills
                    .Where(entry => !string.IsNullOrWhiteSpace(entry.Key) && !string.IsNullOrWhiteSpace(entry.Value))
                    .ToDictionary(entry => entry.Key.Trim(), entry => entry.Value.Trim());

                    if (addSkills.Count > 0)
                    {
                        if(presentSkills == null)
                        {
                            presentSkills = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        }
                        getAboutPageModel.Skills = presentSkills.Union(addSkills)
                            .ToDictionary(entry => entry.Key, entry => entry.Value);
                    }
                    else
                    {
                        throw new Exception("Skills cannot be null or empty.Please add skills");
                    }
                }
                else
                {
                    throw new Exception("Skills cannot be null or empty.Please add skills");
                }

                var collection = _connection.ConnectToMongoDb<AboutModel>(Constants.Constants.AboutCollection);
                var filter = Builders<AboutModel>.Filter.Eq("Id", id);
                return collection.ReplaceOneAsync(filter, getAboutPageModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Skills updated successfully."); ;
            }
            catch (Exception ex)
            {
                return Task.FromResult(ex.Message);
            }

        }

        /// <summary>
        /// This method is used to remove removeSkill.
        /// </summary>
        /// <param name="removeSkill"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> RemoveSkill(string removeSkill,string id)
        {

            try
            {
                var getAboutPageModel = GetAboutPageData(id);
                if(getAboutPageModel.Skills != null)
                {
                    if(string.IsNullOrWhiteSpace(removeSkill.ToLower().Trim()))
                    {
                        throw new Exception("Skill cannot be null or empty");
                    }
                    
                    if(getAboutPageModel.Skills.ContainsKey(removeSkill.Trim()))
                    {
                        getAboutPageModel.Skills.Remove(removeSkill.Trim());
                        if(getAboutPageModel.Skills.Count == 0)
                        {
                            getAboutPageModel.Skills = null;
                        }
                    }
                    else
                    {
                        throw new Exception("Skill not found to remove.");
                    }
                }
                else
                {
                    throw new Exception("No skills found to remove.");
                }
                var collection = _connection.ConnectToMongoDb<AboutModel>(Constants.Constants.AboutCollection);
                var filter = Builders<AboutModel>.Filter.Eq("Id", id);
                return collection.ReplaceOneAsync(filter, getAboutPageModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Skills updated successfully."); ;
            }
            catch(Exception ex)
            {
                return Task.FromResult(ex.Message);
            }    
        }

        ///// <summary>
        ///// This method is used to add hobbies in the list.
        ///// </summary>
        ///// <param name="hobbies"></param>
        ///// <returns></returns>
        //private List<string> AddHobbies(List<string> hobbies)
        //{
        //    if (hobbies != null)
        //    {
        //        hobbies.RemoveAll(hobby => hobby == null);

        //        if(hobbies.Count > 0)
        //        {
        //            return hobbies;
        //        }
        //        return null;
        //    }
        //    return null;
        //}

        ///// <summary>
        ///// This method is used to update the skills
        ///// </summary>
        ///// <param name="hobbies"></param>
        ///// <returns></returns>
        //private List<string> UpdateHobbies(List<string> addHobbies)
        //{
        //    if (addHobbies != null)
        //    {
        //        addHobbies.RemoveAll(hobby => hobby == null);
        //        if (addHobbies.Count == 0)
        //        {
        //            return null;
        //        }
        //        return addHobbies;
        //    }
        //    return null;
        //}
        

        /// <summary>
        /// This method is used to add hobbies
        /// </summary>
        /// <param name="Hobbies"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> AddHobbies(List<string> Hobbies, string id)
        {
            try
            {
                var getAboutPageModel = GetAboutPageData(id);
                List<string> newHobbies = Hobbies.Where(hobby => !string.IsNullOrWhiteSpace(hobby)).Select(hobby => hobby.Trim()).ToList();
                Hobbies = newHobbies;
                if (Hobbies != null && Hobbies.Count > 0 )
                {
                    if(getAboutPageModel.Hobbies == null)
                    {
                        getAboutPageModel.Hobbies = new List<string>();
                    }
                    getAboutPageModel.Hobbies.AddRange(Hobbies);
                }
                else
                {
                    throw new Exception("Hobbies cannot be null or empty.Please add hobbies");
                }

                var collection = _connection.ConnectToMongoDb<AboutModel>(Constants.Constants.AboutCollection);
                var filter = Builders<AboutModel>.Filter.Eq("Id", id);
                return collection.ReplaceOneAsync(filter, getAboutPageModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Hobbies updated successfully."); ;
            }
            catch(Exception ex) 
            {
                return Task.FromResult(ex.Message);
            }

        }

        /// <summary>
        /// This method is used to remove hobby
        /// </summary>
        /// <param name="Hobby"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> RemoveHobby(string Hobby, string id)
        {
            try
            {
                var getAboutPageModel = GetAboutPageData(id);
                Hobby = Hobby.Trim();
                if(!string.IsNullOrWhiteSpace(Hobby) && getAboutPageModel.Hobbies != null)
                {
                    List<string> hobbiesList = getAboutPageModel.Hobbies.FindAll(hobby => hobby.ToLower().Equals(Hobby.ToLower()));
                    if (hobbiesList.Count > 0)
                    {
                        getAboutPageModel.Hobbies.RemoveAll(item => hobbiesList.Contains(item));
                        if(getAboutPageModel.Hobbies.Count == 0)
                        {
                            getAboutPageModel.Hobbies = null;
                        }
                    }
                    else
                    {
                        throw new Exception("Hobby not found.");
                    }
                    var collection = _connection.ConnectToMongoDb<AboutModel>(Constants.Constants.AboutCollection);
                    var filter = Builders<AboutModel>.Filter.Eq("Id", id);
                    return collection.ReplaceOneAsync(filter, getAboutPageModel, new ReplaceOptions { IsUpsert = true }).ContinueWith(_ => "Hobbies updated successfully."); ;
                }
                else
                {
                    throw new Exception("Hobby not found.");
                }
            }
            catch(Exception ex)
            {
                return Task.FromResult(ex.Message);
            }
        }
    }
}

//if (addHobbies != null)
//{
    
//    addHobbies.RemoveAll(Hobby => Hobby == null);
//    if (addHobbies.Count > 0)
//    {
//        if (presentHobbies == null)
//        {
//            presentHobbies = new List<string>();
//        }
//        presentHobbies.AddRange(addHobbies);
//    }
//    return presentHobbies;
//}
//return presentHobbies;