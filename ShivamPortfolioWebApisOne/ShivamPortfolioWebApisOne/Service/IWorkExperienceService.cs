using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service
{
    public interface IWorkExperienceService
    {
        /// <summary>
        /// This method is used to add the new work experience
        /// </summary>
        /// <param name="workExperienceDto"></param>
        /// <returns></returns>
        public Task<string> AddWorkExperience(WorkExperienceDto workExperienceDto);

        /// <summary>
        /// This method is used to update the work experience
        /// </summary>
        /// <param name="workExperienceDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> UpdateWorkExperience(WorkExperienceDto workExperienceDto,string id);

        /// <summary>
        /// This method is used to delete the work experience
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> DeleteWorkExperience(string id);

        /// <summary>
        /// This method is used to delete the particular image of the particular work experience
        /// </summary>
        /// <param name="id"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public Task<string> DeleteParticularImage(string id,int priority);

        /// <summary>
        /// This method is used to get all the work experience
        /// </summary>
        /// <returns></returns>
        public Task<List<WorkExperienceModel>> GetAllWorkExperience();

        /// <summary>
        /// This method is used to get the particular work experience
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkExperienceModel GetWorkExperience(string id);

        /// <summary>
        /// This method is used to remove the logo of the company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> RemoveLogo(string id);
    }
}
