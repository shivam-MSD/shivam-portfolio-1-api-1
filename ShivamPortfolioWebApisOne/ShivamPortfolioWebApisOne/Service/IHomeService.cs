using ShivamPortfolioWebApisOne.Dtos;
using ShivamPortfolioWebApisOne.Model;

namespace ShivamPortfolioWebApisOne.Service
{
    public interface IHomeService
    {
        /// <summary>
        /// This method is used to add all the details of home page
        /// </summary>
        public Task<string> AddHomePageData(HomeDto homeDto);

        /// <summary>
        /// This method is used to update the data of the home page. 
        /// </summary>
        /// <param name="homeDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> UpdateHomeData(HomeDto homeDto,string id);

        /// <summary>
        /// This method is used to remove the image from the image list or image ProjectPriority list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public Task<string> DeleteParticularImage(string id,int priority);

        /// <summary>
        /// This method is used to delete who home data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<string> DeleteHomePageData(string id);

        /// <summary>
        ///  This method is used to get all the detals of the home page.
        /// </summary>
        /// <returns></returns>
        public Task<List<HomeModel>> GetAllHomePageDetails();

        /// <summary>
        /// This method is used to get the data for the particular home page
        /// </summary>
        /// <param name="id"></param>
        public HomeModel GetHomePageData(string id);

    }
}
