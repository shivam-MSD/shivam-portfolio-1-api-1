using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service
{
    public interface IAboutService
    {
        /// <summary>
        /// This method is used to add all the details of about page
        /// </summary>
        public Task<string> AddAboutPageData(AboutDto aboutDto);

        /// <summary>
        /// This method is used to update the data of the about page 
        /// </summary>
        /// <param name="aboutDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> UpdateAboutPageData(AboutDto aboutDto, string id);

        /// <summary>
        /// This method is used to remove the image from the image list or image ProjectPriority list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Priority"></param>
        /// <returns></returns>
        public Task<string> DeleteParticularImage(string id, int Priority);

        /// <summary>
        /// This method is used to delete specific home data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> DeleteAboutPageData(string id);

        /// <summary>
        ///  This method is used to get all the detals of the about page.
        /// </summary>
        /// <returns></returns>
        public Task<List<AboutModel>> GetAllAboutPageDetails();

        /// <summary>
        /// This method is used to get the data for the particular home page
        /// </summary>
        /// <param name="id"></param>
        public AboutModel GetAboutPageData(string id);

        /// <summary>
        /// This method is used to add about me description in the document.
        /// </summary>
        /// <param name="aboutMeDescription"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> AddHobbies(List<string> Hobbies, string id);

        /// <summary>
        /// This method is used to add about career description in the document.
        /// </summary>
        /// <param name="aboutMeDescription"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> RemoveHobby(string hobby,string id);

        /// <summary>
        /// This method is used to add the skills
        /// </summary>
        /// <param name="addSkills"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> AddSkills(Dictionary<string, string> addSkills, string id);

        /// <summary>
        /// This method is used to remove skill
        /// </summary>
        /// <param name="skill"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> RemoveSkill(string skill, string id);

    }
}
